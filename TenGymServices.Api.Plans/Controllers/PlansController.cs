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
        public async Task GetPlans([FromRoute] Get.GetPlans request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{ProductPaypalId:int}")]
        public async Task GetPlansById([FromRoute] Get.GetPlans request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{ProductPaypalId:int}/deactivate")]
        public async Task DesactivePlan([FromRoute] Post.ActivePlanHandler request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{ProductPaypalId:int}/activate")]
        public async Task ActivatePlan([FromRoute] Get.GetPlans request)
        {
            await _mediator.Send(request, new CancellationToken());
        }


    }
}