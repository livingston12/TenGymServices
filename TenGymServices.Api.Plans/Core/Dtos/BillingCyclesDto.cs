using TenGymServices.Api.Plans.Core.Enums;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class BillingCyclesDto
    {
        public TENURE_TYPES TenureType { get; set; }
        public int Sequence { get; set; }
        public int TotalCycles { get; set; }
        public PricingSchemeDto PricingScheme { get; set; }
        public FrequencyDto Frequency { get; set; }
    }
}