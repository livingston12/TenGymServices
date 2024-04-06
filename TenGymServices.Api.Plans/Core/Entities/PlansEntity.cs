using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class PlanEntity
    {
        [Key]
        public int PlanId { get; set; }
        public string PaypalId { get; set; }
        public string ProductPaypalId { get; set; }
        public string Name { get; set; }
        public STATUS Status { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<BillingCycleEntity> BillingCycles { get; set; }
        public PaymentPreferenceEntity PaymentPreference { get; set; }
        public TaxEntity Tax { get; set; }
    }

}