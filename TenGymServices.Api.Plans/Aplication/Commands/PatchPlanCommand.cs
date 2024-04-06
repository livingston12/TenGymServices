using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Commands
{
    public record PatchPlanCommand : IRequest
    {
        public int PlanId { get; set; }
        public JsonPatchDocument<PatchPlanDto> PatchDocument { get; set; }
    }
}