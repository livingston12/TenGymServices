using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class FrequencyDto
    {
        [JsonProperty("interval_unit")]
        [EnumDataType(typeof(INTERVAL_UNIT))]
        [JsonConverter(typeof(StringEnumConverter))]
        public INTERVAL_UNIT IntervalUnit { get; set; }
        [JsonProperty("interval_count")]
        public int IntervalCount { get; set; }
    }
}