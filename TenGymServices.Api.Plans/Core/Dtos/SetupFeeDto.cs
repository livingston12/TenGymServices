namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class SetupFeeDto
    {
        public string CurrencyCode { get; set; } = "USD";
        public string Value { get; set; }
    }
}