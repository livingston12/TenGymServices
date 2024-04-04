using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Interfaces;

namespace TenGymServices.Api.Plans.Aplication.Plans
{
    public class Post
    {
        // Create Plan
        public class CreatePlanRequest : CreatePlansDto, IRequest
        {

        }

        public class CreatePlanRequestHandler : IRequestHandler<CreatePlanRequest>
        {
            public Task Handle(CreatePlanRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }



        // Desactive Plan

        public class DesactivatePlanRequest : IRequest, IPlanId
        {
            public int PlanId { get; set; }
        }


        public class DesactivePlanRequestHandler : IRequestHandler<DesactivatePlanRequest>
        {
            public Task Handle(DesactivatePlanRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        // Active Plan
        public class ActivatePlanRequest : IRequest, IPlanId
        {
            public int PlanId { get; set; }
        }

        public class ActivePlanHandler : IRequestHandler<ActivatePlanRequest>
        {
            public Task Handle(ActivatePlanRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        // Update Pricing
        public class UpdatePricingPlanRequest : UpdatePricingPlanDto, IRequest
        {

        }

        public class UpdatePricingPlanRequestHandler : IRequestHandler<UpdatePricingPlanRequest>
        {
            public Task Handle(UpdatePricingPlanRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}