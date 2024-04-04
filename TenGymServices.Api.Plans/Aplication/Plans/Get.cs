using MediatR;
using TenGymServices.Api.Plans.Core.Dtos;

namespace TenGymServices.Api.Plans.Aplication.Plans
{
    public class Get
    {
        #region Get List Plans
        public class GetPlans : IRequest<List<PlanDto>>
        {
            public string ProductPaypalId { get; set; }
        }

        public class GetPlansById : IRequest<List<PlanDto>>
        {
            public int PlanId { get; set; }
        }
        #endregion

        #region Show plan Details
        public class GetPlansHandler : IRequestHandler<GetPlans, List<PlanDto>>
        {
            public Task<List<PlanDto>> Handle(GetPlans request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class GetPlansByIdHandler : IRequestHandler<GetPlansById, List<PlanDto>>
        {
            public Task<List<PlanDto>> Handle(GetPlansById request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

    }
}