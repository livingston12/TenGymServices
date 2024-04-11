using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class PaymentPreferencesDto
    {
        [JsonProperty("auto_bill_outstanding")]
        public bool AutoBillOutstanding { get; set; }
        [JsonProperty("setup_fee_failure_action")]
        [EnumDataType(typeof(SETUP_FEE_FAILURE_ACTION))]
        [JsonConverter(typeof(StringEnumConverter))]
        public SETUP_FEE_FAILURE_ACTION SetupFeeFailureAction { get; set; }
        [JsonProperty("payment_failure_threshold")]
        public int PaymentFailureThreshold { get; set; }
        [JsonProperty("setup_fee")]
        public SetupFeeDto SetupFee { get; set; }
    }
}