using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Core.Dtos
{
    public class PlanDto : CreatePlansDto, IPlanId
    {
        public string PlanId {get; set;}
    }
}