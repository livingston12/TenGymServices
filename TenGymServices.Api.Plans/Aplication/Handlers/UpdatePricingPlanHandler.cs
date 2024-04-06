using System.Net;
using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Commands.Validators;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class UpdatePricingPlanHandler : IRequestHandler<UpdatePricingPlanCommand>
    {
        private readonly IPaypalPlansService<UpdatePricingPlanCommand> _paypalService;
        private readonly IRabbitEventBus _rabbitEventBus;
         private readonly IMediator _mediator;

        public UpdatePricingPlanHandler(IPaypalPlansService<UpdatePricingPlanCommand> paypalService,
            IRabbitEventBus rabbitEventBus,
            IMediator mediator
        )
        {
            _paypalService = paypalService;
            _rabbitEventBus = rabbitEventBus;
            _mediator = mediator;
        }

        public async Task Handle(UpdatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            request.ValidateRequest<UpdatePricingPlanCommand, UpatePricingPlanValidator>();
            var plan = await _mediator.Send(new GetByIdPlanQuery() { PlanId = request.PlanId}, cancellationToken);
            
            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{plan.PaypalId}/update-pricing-schemes");
            if (responsePaypal.hasEerror)
            {
               request.ThrowHttpHandlerExeption(responsePaypal.MessageError, HttpStatusCode.BadRequest);
            }
            UpdatePricingPlanQuee dataQuee = new UpdatePricingPlanQuee
            {
                ListPricingPlans = request.Select((item, index) => new UpdatePricingSchemePlanQuee()
                {
                    BillingCycleSequence = index + 1,
                    PricingScheme = item.PricingScheme
                }).ToList()
            };

            _rabbitEventBus.Publish(dataQuee);
        }
    }
}