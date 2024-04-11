using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Plans.RabbitMq.Queues
{
    public class ActivatePlanQuee : Event, IPlanId
    {
        public int PlanId {get; set;}
    }
}