using Newtonsoft.Json;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class SetupFeeDto
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; } = "USD";
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}