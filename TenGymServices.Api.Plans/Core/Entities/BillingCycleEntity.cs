using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class BillingCycleEntity
    {
        [Key]
        public int BillingCycleId { get; set; }
        public int PlanId { get; set; }
        [Required, Range(1, 24)]
        [EnumDataType(typeof(TENURE_TYPES))]
        public string TenureType { get; set; }
        [Required, Range(1, 99)]
        public int Sequence { get; set; }
        [Range(0, 999)]
        public int TotalCycles { get; set; }
        public PricingSchemeEntity PricingScheme { get; set; }
        public FrequencyEntity Frequency { get; set; }
        public PlanEntity Plan { get; set; }
    }
}