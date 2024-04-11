using System.Net;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Enums;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Persistence;

namespace TenGymServices.Api.Plans.RabbitMq.Consumers
{
    public class ActivatePlanConsumer : IConsumer<ActivatePlanQuee>
    {
        private readonly IRepositoryDB _planRepository;
        private readonly PlanContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ActivatePlanConsumer(
            IRepositoryDB planRepository,
            IMediator mediator,
            PlanContext context,
            IMapper mapper)
        {
            _planRepository = planRepository;
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ActivatePlanQuee> context)
        {
            await ActivatePlanDB(context.Message);
        }

        private async Task ActivatePlanDB(ActivatePlanQuee request)
        {
            var planEntity = await _context.Plans.FirstOrDefaultAsync(x => x.PlanId == request.PlanId);
            planEntity.Status = STATUS.ACTIVE.ToString();
            
            _planRepository.UpdateAsync(_context, planEntity);

            var inserted = await _planRepository.Commit(_context);
            if (inserted == 0)
            {
                request.ThrowHttpHandlerExeption("Cannot activate the plan", HttpStatusCode.BadRequest);
            }
        }
    }
}