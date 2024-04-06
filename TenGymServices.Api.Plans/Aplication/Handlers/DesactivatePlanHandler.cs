using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.RabbitMq.Bus.BusRabbit;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class DesactivatePlanHandler : IRequestHandler<DesactivatePlanCommand>
    {
        private readonly IPaypalPlansService<DesactivatePlanCommand> _paypalService;
        private readonly IRabbitEventBus _rabbitEventBus;

        public DesactivatePlanHandler(IPaypalPlansService<DesactivatePlanCommand> paypalService,
            IRabbitEventBus rabbitEventBus
        )
        {
            _paypalService = paypalService;
            _rabbitEventBus = rabbitEventBus;
        }
        public async Task Handle(DesactivatePlanCommand request, CancellationToken cancellationToken)
        {
            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{request.PlanId}/deactivate");
            if (responsePaypal.hasEerror)
            {
                throw new Exception(responsePaypal.MessageError);
            }

            var dataQuee = new DesactivatePlanQuee() { PlanPaypalId = responsePaypal.Id };
            _rabbitEventBus.Publish(dataQuee);
        }
    }
}