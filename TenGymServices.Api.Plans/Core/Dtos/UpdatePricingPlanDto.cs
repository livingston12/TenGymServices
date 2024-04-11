using System.Text.Json;
using Newtonsoft.Json;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class UpdatePricingPlanDto
    {
        [JsonProperty("pricing_schemes")]
        public List<UpdatePricingSchemePlanDto> PricingSchemes { get; set; }
    }

   
}