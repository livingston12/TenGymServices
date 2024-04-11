using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class BillingCyclesDto
    {
        [JsonProperty("tenure_type")]
        [EnumDataType(typeof(TENURE_TYPES))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TENURE_TYPES TenureType { get; set; }
        [JsonProperty("sequence")]
        public int Sequence { get; set; }
        [JsonProperty("total_cycles")]
        public int TotalCycles { get; set; }
        [JsonProperty("pricing_scheme")]
        public PricingSchemeDto PricingScheme { get; set; }
        [JsonProperty("frequency")]
        public FrequencyDto Frequency { get; set; }
    }
}