using System.Net;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Core.Enums;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Persistence;

namespace TenGymServices.Api.Plans.RabbitMq.Consumers;

public class DesactivatePlanConsumer : IConsumer<DesactivatePlanQuee>
{
    private readonly PlanContext _context;
    private readonly IRepositoryDB _planRepository;

    public DesactivatePlanConsumer(PlanContext context, IRepositoryDB planRepository)
    {
        _context = context;
        _planRepository = planRepository;
    }

    public async Task Consume(ConsumeContext<DesactivatePlanQuee> context)
    {
        await DesactivatePlanDB(context.Message);
    }

    private async Task DesactivatePlanDB(DesactivatePlanQuee request)
    {
        var planEntity = await _context.Plans.FirstOrDefaultAsync(x => x.PlanId == request.PlanId);
        planEntity.Status = STATUS.INACTIVE.ToString();

        _planRepository.UpdateAsync(_context, planEntity);

        var inserted = await _planRepository.Commit(_context);
        if (inserted == 0)
        {
            request.ThrowHttpHandlerExeption("Cannot desactivate the plan", HttpStatusCode.BadRequest);
        }
    }
}
