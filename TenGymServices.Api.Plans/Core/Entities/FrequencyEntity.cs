using System.ComponentModel.DataAnnotations;
using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class FrequencyEntity
    {
        [Key]
        public int FrequencyId { get; set; }
        public int BillingCycleId { get; set; }
        public INTERVAL_UNIT IntervalUnit { get; set; }
        public int IntervalCount { get; set; }
        public BillingCycleEntity billingCycle { get; set; }
    }
}