using System.ComponentModel.DataAnnotations;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class SetupFeeEntity
    {
        [Key]
        public int SetupFeeId { get; set; }
        public int PaymentPreferenceId { get; set; }
        [Required, StringLength(maximumLength: 3, MinimumLength= 3)]
        public string CurrencyCode { get; set; }
        [Required, MaxLength(32)]
        public string Value { get; set; }
        public PaymentPreferenceEntity PaymentPreference { get; set; }
    }
}