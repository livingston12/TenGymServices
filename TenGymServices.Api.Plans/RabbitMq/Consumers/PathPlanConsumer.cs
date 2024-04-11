using System.Net;
using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Plans.RabbitMq.Consumers
{
    public class PathPlanConsumer : IConsumer<PatchPlanQuee>
    {
        private readonly PlanContext _context;
        private readonly IMapper _mapper;

        public PathPlanConsumer(PlanContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PatchPlanQuee> context)
        {
            await patchPlan(context.Message);
        }

        private async Task patchPlan(PatchPlanQuee request)
        {
            var plan = await _context.Plans.FirstOrDefaultAsync(x => x.PlanId == request.PlanId);

            if (plan == null)
            {
                request.ThrowHttpHandlerExeption("Plan does not exist", HttpStatusCode.NotFound);
            }
            _mapper.Map(request.PatchPlan, plan);
            await _context.SaveChangesAsync();
        }
    }
}