using System.Net;
using AutoMapper;
using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class PatchPlanHandler : IRequestHandler<PatchPlanCommand>
    {
        private readonly IPaypalPlansService<PatchPlanCommand> _paypalService;
        private readonly IMapper _mapper;
        private readonly IRabbitEventBus _rabbitEventBus;
        private readonly IMediator _mediator;
        private readonly ILogger<PatchPlanHandler> _logger;

        public PatchPlanHandler(IPaypalPlansService<PatchPlanCommand> paypalService,
            IMapper mapper,
            IRabbitEventBus rabbitEventBus,
            IMediator mediator,
            ILogger<PatchPlanHandler> logger)
        {
            _paypalService = paypalService;
            _mapper = mapper;
            _rabbitEventBus = rabbitEventBus;
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
                request.ThrowHttpHandlerExeption("Product does not exist", HttpStatusCode.NotFound);
            }

            var patchPlanDto = _mapper.Map<PatchPlanDto>(plan);
            try
            {
                var PatchPlan = await _paypalService.PatchPlan(plan.PaypalId, patchPlanDto);
                if (PatchPlan.hasEerror == null)
                {
                    request.ThrowHttpHandlerExeption(PatchPlan.MessageError, HttpStatusCode.BadRequest);
                }
                var planQuee = _mapper.Map<PatchPlanQuee>(request);
                
                _rabbitEventBus.Publish(planQuee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                request.ThrowHttpHandlerExeption("Internal Server Error please contact support", HttpStatusCode.InternalServerError);
            }
        }
    }
}