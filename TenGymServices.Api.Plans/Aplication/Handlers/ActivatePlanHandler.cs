using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.RabbitMq.Bus.BusRabbit;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class ActivatePlanHandler : IRequestHandler<ActivatePlanCommand>
    {
        private readonly IPaypalPlansService<ActivatePlanCommand> _paypalService;
        private readonly IRabbitEventBus _rabbitEventBus;

        public ActivatePlanHandler(IPaypalPlansService<ActivatePlanCommand> paypalService,
            IRabbitEventBus rabbitEventBus
        )
        {
            _paypalService = paypalService;
            _rabbitEventBus = rabbitEventBus;
        }

        public async Task Handle(ActivatePlanCommand request, CancellationToken cancellationToken)
        {
            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{request.PlanId}/activate");
            if (responsePaypal.hasEerror)
            {
                throw new Exception(responsePaypal.MessageError);
            }

            var dataQuee = new ActivatePlanQuee() { PlanPaypalId = responsePaypal.Id};
            _rabbitEventBus.Publish(dataQuee);
        }
    }
}