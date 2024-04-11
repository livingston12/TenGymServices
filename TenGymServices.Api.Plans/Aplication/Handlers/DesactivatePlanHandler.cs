using System.Net;
using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class DesactivatePlanHandler : IRequestHandler<DesactivatePlanCommand>
    {
        private readonly IPaypalPlansService<DesactivatePlanCommand> _paypalService;
        private readonly IMassTransientBus _massTransientEventBus;
        private readonly IMediator _mediator;

        public DesactivatePlanHandler(IPaypalPlansService<DesactivatePlanCommand> paypalService,
            IMassTransientBus massTransientEventBus
,
            IMediator mediator)
        {
            _paypalService = paypalService;
            _massTransientEventBus = massTransientEventBus;
            _mediator = mediator;
        }
        public async Task Handle(DesactivatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _mediator.Send(new GetByIdPlanQuery{ PlanId = request.PlanId}, cancellationToken);

            var responsePaypal = await _paypalService.PostAsync(request, $"/v1/billing/plans/{plan.PaypalId}/deactivate");
            if (responsePaypal.hasEerror)
            {
                request.ThrowHttpHandlerExeption(responsePaypal.MessageError, HttpStatusCode.BadRequest);
            }

            var dataQuee = new DesactivatePlanQuee() { PlanId = plan.PlanId };
            _massTransientEventBus.Publish(dataQuee);
        }
    }
}