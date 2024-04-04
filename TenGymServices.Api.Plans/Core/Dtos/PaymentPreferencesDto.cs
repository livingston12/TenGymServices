using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class PaymentPreferencesDto
    {
        public bool AutoBillOutstanding { get; set; }
        public SETUP_FEE_FAILURE_ACTION SetupFeeFailureAction { get; set; }
        public int PaymentFailureThreshold { get; set; }
        public SetupFeeDto SetupFee { get; set; }
    }
}