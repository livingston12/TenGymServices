using System.ComponentModel.DataAnnotations;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class FixedPriceEntity
    {
        [Key]
        public int FixedPriceId { get; set; }
        public int PricingSchemeId { get; set; }
        [Required, StringLength(3, MinimumLength = 3)]
        public string CurrencyCode { get; set; }
        [Required, MaxLength(32)]
        public string Value { get; set; }
        public PricingSchemeEntity PricingScheme { get; set; }
    }
}