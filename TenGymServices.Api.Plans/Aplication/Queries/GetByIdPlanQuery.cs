using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Queries
{
    public record GetByIdPlanQuery(int planId) : IRequest<PlanDto>;
}