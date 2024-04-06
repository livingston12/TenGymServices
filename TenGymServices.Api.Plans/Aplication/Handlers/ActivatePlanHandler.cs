using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.RabbitMq.Bus.BusRabbit;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class ActivatePlanHandler : IRequestHandler<ActivatePlanCommand>
    {
        private readonly IPaypalPlansService<ActivatePlanCommand> _paypalService;
        private readonly IRabbitEventBus _rabbitEventBus;
        private readonly IMediator _mediator;

        public ActivatePlanHandler(IPaypalPlansService<ActivatePlanCommand> paypalService,
            IRabbitEventBus rabbitEventBus,
            IMediator mediator)
        {
            _paypalService = paypalService;
            _rabbitEventBus = rabbitEventBus;
            _mediator = mediator;
        }

        public async Task Handle(ActivatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _mediator.Send(new GetByIdPlanQuery{ PlanId = request.PlanId}, cancellationToken);
           
            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{plan.PaypalId}/activate");
            if (responsePaypal.hasEerror)
            {
                throw new Exception(responsePaypal.MessageError);
            }

            var dataQuee = new ActivatePlanQuee() { PlanPaypalId = responsePaypal.Id};
            _rabbitEventBus.Publish(dataQuee);
        }
    }
}