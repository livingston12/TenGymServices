using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class PlanEntity
    {
        [Key]
        public int PlanId { get; set; }
        public string PaypalId { get; set; }
        [Required]
        [Range(6, 50)]
        public string ProductPaypalId { get; set; }
        [Required]
        [MaxLength(127)]
        public string Name { get; set; }
        [EnumDataType(typeof(STATUS))]
        [MaxLength(14)]
        public string Status { get; set; }
        [MaxLength(127)]
        public string Description { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public List<BillingCycleEntity> BillingCycles { get; set; }
        public PaymentPreferenceEntity PaymentPreference { get; set; }
        public TaxEntity? Tax { get; set; }
    }

}