using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TenGymServices.Api.Plans.Core.Enums;
using TenGymServices.RabbitMq.Bus.Events;
using Newtonsoft.Json.Converters;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class CreatePlansDto : Event
    {
        [JsonProperty("product_id")]
        public string ProductPaypalId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [EnumDataType(typeof(STATUS))]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public STATUS Status { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("billing_cycles")]
        public BillingCyclesDto[] BillingCycles { get; set; }
        
        [JsonProperty("payment_preferences")]
        public PaymentPreferencesDto? PaymentPreference { get; set; }
        [JsonProperty("taxes")]
        public TaxDto? Taxes { get; set; }

    }

}