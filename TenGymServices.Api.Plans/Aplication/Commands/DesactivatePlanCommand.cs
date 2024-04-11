using MediatR;
using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Aplication.Commands
{
    public record DesactivatePlanCommand : IRequest, IPlanId
    {
        public int PlanId {get; set;}
    }
}