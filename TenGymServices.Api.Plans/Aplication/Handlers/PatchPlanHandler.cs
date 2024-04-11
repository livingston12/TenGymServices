using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class PatchPlanHandler : IRequestHandler<PatchPlanCommand>
    {
        private readonly IPaypalPlansService<JsonPatchDocument<PatchPlanDto>> _paypalService;
        private readonly IMapper _mapper;
        private readonly IMassTransientBus _massTransientBus;
        private readonly IMediator _mediator;
        private readonly ILogger<PatchPlanHandler> _logger;

        public PatchPlanHandler(IPaypalPlansService<JsonPatchDocument<PatchPlanDto>> paypalService,
            IMapper mapper,
            IMassTransientBus massTransientBus,
            IMediator mediator,
            ILogger<PatchPlanHandler> logger)
        {
            _paypalService = paypalService;
            _mapper = mapper;
            _massTransientBus = massTransientBus;
            _mediator = mediator;
            _logger = logger;
        }


        public async Task Handle(PatchPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _mediator.Send(new GetByIdPlanQuery() { PlanId = request.PlanId }, cancellationToken);

            if (request.PatchDocument == null)
            {
                request.ThrowHttpHandlerExeption("Please verify your body request", HttpStatusCode.BadRequest);
            }

            if (plan == null)
            {
                request.ThrowHttpHandlerExeption("Plan does not exist", HttpStatusCode.NotFound);
            }
            

            var PatchPlanPaypal = await _paypalService.PostAsync(request.PatchDocument, $"/v1/billing/plans/{plan.PaypalId}", HttpMethod.Patch);

            if (PatchPlanPaypal.hasEerror)
            {
                request.ThrowHttpHandlerExeption(PatchPlanPaypal.MessageError, HttpStatusCode.BadRequest);
            }

            var patchPlanDto = _mapper.Map<PatchPlanDto>(plan);
            request.PatchDocument.ApplyTo(patchPlanDto);

            var planQuee = new PatchPlanQuee()
            {
                PlanId = request.PlanId,
                PatchPlan = patchPlanDto
            };

            _massTransientBus.Publish(planQuee);
        }
    }
}