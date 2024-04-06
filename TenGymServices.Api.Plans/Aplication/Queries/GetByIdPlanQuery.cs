using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Queries
{
    public class GetByIdPlanQuery: IRequest<PlanDto> 
    {
        public int PlanId { get; set; }
    }
}