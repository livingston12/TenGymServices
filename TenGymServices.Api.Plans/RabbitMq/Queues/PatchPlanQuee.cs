using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Plans.RabbitMq.Queues
{
    public class PatchPlanQuee : Event
    {
        public int PlanId { get; set; }
        public PatchPlanDto PatchPlan { get; set; }
    }
}