namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class PatchPlanDto
    {
       public string Description { get; set; }
       public string Name { get; set; }
       public PaymentPreferencesDto PaymentPreference { get; set; }
       public PatchTaxDto Tax { get; set; }
    }
}