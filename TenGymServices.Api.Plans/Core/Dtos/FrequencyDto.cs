using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class FrequencyDto
    {
        public INTERVAL_UNIT IntervalUnit { get; set; }
        public int IntervalCount { get; set; }
    }
}