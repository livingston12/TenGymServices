using Newtonsoft.Json;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class UpdatePricingSchemePlanDto
    {
        
        [JsonProperty("billing_cycle_sequence")]
        public int BillingCycleSequence { get; set; }
        [JsonProperty("pricing_scheme")]
        public PricingSchemeDto PricingScheme { get; set; }
    }
}