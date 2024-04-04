using System.ComponentModel.DataAnnotations;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class TaxesEntity
    {
        [Key]
        public string TaxId { get; set; }
        public int PlanId { get; set; }
        public string Percentage { get; set; }
        public string Inclusive { get; set; }
        public PlansEntity Plan { get; set; }
    }
}