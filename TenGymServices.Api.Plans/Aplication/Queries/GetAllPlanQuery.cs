using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Queries
{
    public record GetAllPlanQuery : IRequest<List<PlanDto>> 
    {
        public string ProductPaypalId { get; init; } = string.Empty;
    }
}