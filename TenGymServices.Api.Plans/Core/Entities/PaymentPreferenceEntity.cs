using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class PaymentPreferenceEntity
    {
        [Key]
        public int PaymentPreferenceId { get; set; }
        public int PlanId { get; set; }
        public bool AutoBillOutstanding { get; set; }
        public SETUP_FEE_FAILURE_ACTION SetupFeeFailureAction { get; set; }
        public int PaymentFailureThreshold { get; set; }
        public SetupFeeEntity SetupFee { get; set; }
        public PlanEntity Plan { get; set; }
    }
}