using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class PlansEntity
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
        public List<BillingCyclesEntity> BillingCycles { get; set; }
        public PaymentPreferencesEntity PaymentPreference { get; set; }
        public TaxesEntity Tax { get; set; }
    }

}