using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Queries
{
    public record GetAllPlanQuery(string ProductPaypalId) : IRequest<List<PlanDto>>;
}