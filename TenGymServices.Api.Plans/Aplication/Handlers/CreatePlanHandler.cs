using System.Net;
using AutoMapper;
using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Commands.Validators;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class CreatePlanHandler : IRequestHandler<CreatePlanCommand>
    {
        private readonly IPaypalPlansService<CreatePlanCommand> _paypalService;
        private readonly IMapper _mapper;
        private readonly IMassTransientBus _massTransientEventBus;

        public CreatePlanHandler(IPaypalPlansService<CreatePlanCommand> paypalService,
            IMapper mapper,
            IMassTransientBus rabbitEventBus
        )
        {
            _paypalService = paypalService;
            _mapper = mapper;
            _massTransientEventBus = rabbitEventBus;
        }

        public async Task Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            request.ValidateRequest<CreatePlanCommand, CreatePlanValidator>();
            var responsePaypal = await _paypalService.PostAsync(request, "/v1/billing/plans/");
            if (responsePaypal.hasEerror)
            {
                request.ThrowHttpHandlerExeption(responsePaypal.MessageError, HttpStatusCode.BadRequest);
            }

            var dataQuee = _mapper.Map<CreatePlanCommand, PlanEventQuee>(request);
            dataQuee.PaypalId = responsePaypal.Id;
            _massTransientEventBus.Publish(dataQuee);
            //_rabbitEventBus.Publish(dataQuee);
        }
    }
}