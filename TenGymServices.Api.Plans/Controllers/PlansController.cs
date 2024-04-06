using MediatR;
using Microsoft.AspNetCore.Mvc;
using TenGymServices.Api.Plans.Aplication.Commands;
using TenGymServices.Api.Plans.Aplication.Queries;

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

        [HttpGet("{ProductPaypalId}")]
        public async Task GetPlans([FromQuery] GetAllPlanQuery request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{PlanId:int}")]
        public async Task GetPlansById([FromRoute] GetByIdPlanQuery request)
        {
            await _mediator.Send(request, new CancellationToken());
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


    }
}