namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class UpdatePricingPlanDto
    {
       public int BillingCycleSequence { get; set; } = 1;
       public string PricingScheme { get; set; }
       public FixedPriceDto FixedPrice { get; set; }
    }
}