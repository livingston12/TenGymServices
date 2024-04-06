using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.Api.Plans.EventQuee
{
    public class UpdatePricingPlanQuee : Event, IPlanId
    {

        public List<UpdatePricingSchemePlanQuee> ListPricingPlans { get; set; }
        public string PlanId { get; set; }
    }

    public class UpdatePricingSchemePlanQuee
    {
        public int BillingCycleSequence { get; set; }
        
       public PricingSchemeDto PricingScheme { get; set; }
    }
}