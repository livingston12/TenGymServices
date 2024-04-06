using MediatR;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class GetByIdPlanHandler : IRequestHandler<GetByIdPlanQuery, PlanDto>
    {
        public Task<PlanDto> Handle(GetByIdPlanQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}