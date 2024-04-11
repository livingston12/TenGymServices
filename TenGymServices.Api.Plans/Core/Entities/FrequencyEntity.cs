using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class FrequencyEntity
    {
        [Key]
        public int FrequencyId { get; set; }
        public int BillingCycleId { get; set; }
        [EnumDataType(typeof(INTERVAL_UNIT))]
        [Required, Range(1, 24)]
        public string IntervalUnit { get; set; }
        [Range(1, 365)]
        public int IntervalCount { get; set; } = 1;
        public BillingCycleEntity billingCycle { get; set; }
    }
}