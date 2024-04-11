using MediatR;
using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Aplication.Commands
{
    public record ActivatePlanCommand : IRequest, IPlanId
    {
        public int PlanId {get; set;}
    }
}