using System.Net;
using AutoMapper;
using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Commands.Validators;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class UpdatePricingPlanHandler : IRequestHandler<UpdatePricingPlanCommand>
    {
        private readonly IPaypalPlansService<UpdatePricingPlanCommand> _paypalService;
        private readonly IMassTransientBus _massTransientBus;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdatePricingPlanHandler(IPaypalPlansService<UpdatePricingPlanCommand> paypalService,
            IMassTransientBus massTransientBus,
            IMediator mediator,
            IMapper mapper)
        {
            _paypalService = paypalService;
            _massTransientBus = massTransientBus;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(UpdatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            request.ValidateRequest<UpdatePricingPlanCommand, UpatePricingPlanValidator>();
            var plan = await _mediator.Send(new GetByIdPlanQuery() { PlanId = request.PlanId }, cancellationToken);

            var paypalRequest = new UpdatePricingPlanCommand
            {
                PlanId = request.PlanId,
                PricingSchemes = request?.PricingSchemes
                    .Select((item, index) => new UpdatePricingSchemePlanDto()
                    {
                        BillingCycleSequence = index + 1,
                        PricingScheme = item.PricingScheme
                    })
                    .ToList()
            };
            if (paypalRequest == null)
            {
                request.ThrowHttpHandlerExeption("Incorrect parse of parameters", HttpStatusCode.BadRequest);
            }


            var responsePaypal = await _paypalService.PostAsync(paypalRequest, $"/v1/billing/plans/{plan.PaypalId}/update-pricing-schemes");
            if (responsePaypal.hasEerror)
            {
                request.ThrowHttpHandlerExeption(responsePaypal.MessageError, HttpStatusCode.BadRequest);
            }

            var dataQuee = _mapper.Map<UpdatePricingPlanCommand, UpdatePricingPlanQuee>(paypalRequest);
            _massTransientBus.Publish(dataQuee);
        }
    }
}