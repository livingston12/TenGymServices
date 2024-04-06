using MediatR;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public record GetAllPlanHandler : IRequestHandler<GetAllPlanQuery, List<PlanDto>>
    {
        public Task<List<PlanDto>> Handle(GetAllPlanQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}