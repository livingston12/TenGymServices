using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Controllers
{
    [ApiController]
    [Route("/api/plans")]
    public class PlansController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task CreatePlan(CreatePlanCommand request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet()]
        public async Task GetPlans([FromHeader] GetAllPlanQuery request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{PlanId:int}")]
        public async Task<PlanDto> GetPlansById([FromRoute] GetByIdPlanQuery request)
        {
            return await _mediator.Send(request, new CancellationToken());
        }

        [HttpPost("{PlanId:int}/deactivate")]
        public async Task DesactivePlan([FromRoute] DesactivatePlanCommand request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpPost("{PlanId:int}/activate")]
        public async Task ActivatePlan([FromRoute] ActivatePlanCommand request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpPatch("{planId:int}")]
        public async Task PatchPlan([FromRoute] int planId, [FromBody] JsonPatchDocument<PatchPlanDto> request)
        {
            var patchRequest = new PatchPlanCommand
            {
                PlanId = planId,
                PatchDocument = request
            };
            await _mediator.Send(patchRequest, new CancellationToken());
        }

        [HttpPost("{planId:int}/updatepricing")]
        public async Task UpdatePrincingPlan([FromRoute] int planId, [FromBody] UpdatePricingPlanDto request)
        {
            var command = new UpdatePricingPlanCommand()
            {
                PlanId = planId,
                PricingSchemes = request.PricingSchemes
            };

            await _mediator.Send(command, new CancellationToken());
        }
    }
}