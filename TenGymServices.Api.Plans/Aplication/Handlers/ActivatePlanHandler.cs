using System.Net;
using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class ActivatePlanHandler : IRequestHandler<ActivatePlanCommand>
    {
        private readonly IPaypalPlansService<ActivatePlanCommand> _paypalService;
        private readonly IMassTransientBus _transientEventBus;
        private readonly IMediator _mediator;

        public ActivatePlanHandler(IPaypalPlansService<ActivatePlanCommand> paypalService,
            IMassTransientBus transientEventBus,
            IMediator mediator)
        {
            _paypalService = paypalService;
            _transientEventBus = transientEventBus;
            _mediator = mediator;
        }

        public async Task Handle(ActivatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _mediator.Send(new GetByIdPlanQuery{ PlanId = request.PlanId}, cancellationToken);
           
            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{plan.PaypalId}/activate");
            if (responsePaypal.hasEerror)
            {
                request.ThrowHttpHandlerExeption(responsePaypal.MessageError, HttpStatusCode.BadRequest);
            }

            var dataQuee = new ActivatePlanQuee() { PlanId = plan.PlanId};
            _transientEventBus.Publish(dataQuee);
        }
    }
}