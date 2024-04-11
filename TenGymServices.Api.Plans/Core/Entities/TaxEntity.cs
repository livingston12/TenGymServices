using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TenGymServices.Api.Plans.Core.Entities
{
    public class TaxEntity
    {
        [Key]
        public string TaxId { get; set; }
        public int PlanId { get; set; }
        public string Percentage { get; set; }
        public bool Inclusive { get; set; } = true;
        public PlanEntity? Plan { get; set; }
    }
}