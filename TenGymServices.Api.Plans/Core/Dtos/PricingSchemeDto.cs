using Newtonsoft.Json;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class PricingSchemeDto
    {
        [JsonProperty("fixed_price")]
        public FixedPriceDto FixedPrice { get; set; }
    }
}