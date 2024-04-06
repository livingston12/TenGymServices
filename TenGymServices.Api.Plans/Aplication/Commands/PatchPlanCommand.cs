using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Aplication.Commands
{
    public record PatchPlanCommand : IRequest, IPlanId
    {
        public int PlanId { get; set; }
        public JsonPatchDocument<PatchPlanDto> PatchDocument { get; set; }
    }
}