using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Plans.RabbitMq.Queues
{
    public class UpdatePricingPlanQuee : Event, IPlanId
    {
        public List<UpdatePricingSchemePlanDto> ListPricingPlans { get; set; }
        public int PlanId { get; set; }
    }
}