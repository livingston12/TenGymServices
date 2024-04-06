using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Plans.EventQuee
{
    public class ActivatePlanQuee : Event
    {
        public string PlanPaypalId {get; set;}
    }
}