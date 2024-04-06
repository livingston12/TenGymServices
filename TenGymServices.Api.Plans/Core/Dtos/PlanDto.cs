using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class PlanDto : CreatePlansDto
    {
        public string PaypalId { get; set; }
        public int PlanId {get; set;}
    }
}