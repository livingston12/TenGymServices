using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class BillingCyclesEntity
    {
        [Key]
        public int BillingCycleId { get; set; }
        public int PlanId { get; set; }
        public TENURE_TYPES TenureType { get; set; }
        public int Sequence { get; set; }
        public int TotalCycles { get; set; }
        public PricingSchemeEntity PricingScheme { get; set; }
        public FrequencyEntity Frequency { get; set; }
        public PlansEntity Plan { get; set; }
    }
}