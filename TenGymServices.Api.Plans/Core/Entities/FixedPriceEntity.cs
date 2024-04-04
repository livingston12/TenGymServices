using System.ComponentModel.DataAnnotations;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class FixedPriceEntity
    {
        [Key]
        public int FixedPriceId { get; set; }
        public int PricingSchemeId { get; set; }
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
        public PricingSchemeEntity PricingScheme { get; set; }
    }
}