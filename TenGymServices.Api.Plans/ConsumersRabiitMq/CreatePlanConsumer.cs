using System.Net;
using AutoMapper;
using MassTransit;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.EventQuee;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Persistence;

namespace TenGymServices.Api.Plans.ConsumersRabiitMq
{
    public class CreatePlanConsumer : IConsumer<PlanEventQuee>
    {
        private readonly PlanContext _context;
        private readonly IMapper _mapper;
        private readonly IRepositoryDB _planRepository;

        public CreatePlanConsumer(
            IMapper mapper,
            IRepositoryDB planRepository,
            PlanContext context)
        {
            _mapper = mapper;
            _planRepository = planRepository;
            _context = context;
        }
        public async Task Consume(ConsumeContext<PlanEventQuee> context)
        {
            await InsertPlanDB(context.Message);
        }

        private async Task InsertPlanDB(PlanEventQuee request)
        {
            var entity = _mapper.Map<PlanEventQuee, PlanEntity>(request);
            var inserted = await _planRepository.AddAsync(_context ,entity);
            if (inserted == 0)
            {
                request.ThrowHttpHandlerExeption("Cannot insert the the plan", HttpStatusCode.BadRequest);
            }
        }
    }
}