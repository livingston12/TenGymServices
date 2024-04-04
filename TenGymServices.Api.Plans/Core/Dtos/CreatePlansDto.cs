using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class CreatePlansDto
    {
        public string ProductPaypalId { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(STATUS))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public STATUS Status { get; set; }
        public string Description { get; set; }
        public List<BillingCyclesDto> BillingCycles { get; set; }
        public PaymentPreferencesDto PaymentPreference { get; set; }

    }

}