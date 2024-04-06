using System.ComponentModel.DataAnnotations;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class SetupFeeEntity
    {
        [Key]
        public int SetupFeeId { get; set; }
        public int PaymentPreferenceId { get; set; }
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
        public PaymentPreferenceEntity PaymentPreference { get; set; }
    }
}