using System.ComponentModel.DataAnnotations;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class PricingSchemeEntity
    {
        [Key]
        public int PricingSchemeId { get; set; }
        public int BillingCycleId { get; set; }
        public FixedPriceEntity FixedPrice { get; set; }
        public BillingCycleEntity BillingCycle { get; set; }
    }
}