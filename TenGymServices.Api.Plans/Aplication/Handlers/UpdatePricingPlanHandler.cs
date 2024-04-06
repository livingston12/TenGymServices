using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.RabbitMq.Bus.BusRabbit;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class UpdatePricingPlanHandler : IRequestHandler<UpdatePricingPlanCommand>
    {
        private readonly IPaypalPlansService<UpdatePricingPlanCommand> _paypalService;
        private readonly IRabbitEventBus _rabbitEventBus;

        public UpdatePricingPlanHandler(IPaypalPlansService<UpdatePricingPlanCommand> paypalService,
            IRabbitEventBus rabbitEventBus
        )
        {
            _paypalService = paypalService;
            _rabbitEventBus = rabbitEventBus;
        }

        public async Task Handle(UpdatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{request.PlanId}/update-pricing-schemes");
            if (responsePaypal.hasEerror)
            {
                throw new Exception(responsePaypal.MessageError);
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