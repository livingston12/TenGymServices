using MediatR;
using Microsoft.AspNetCore.Mvc;
using TenGymServices.Api.Plans.Aplication.Plans;

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
        public async Task CreatePlan(Post.CreatePlanRequest request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{ProductPaypalId}")]
        public async Task GetPlans([FromQuery] Get.GetPlans request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{PlanId:int}")]
        public async Task GetPlansById([FromRoute] Get.GetPlansById request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpPost("{PlanId:int}/deactivate")]
        public async Task DesactivePlan([FromRoute] Post.DesactivatePlanRequest request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpPost("{PlanId:int}/activate")]
        public async Task ActivatePlan([FromRoute] Post.ActivatePlanRequest request)
        {
            await _mediator.Send(request, new CancellationToken());
        }


    }
}