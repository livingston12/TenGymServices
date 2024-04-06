using MediatR;
using TenGymServices.Api.Plans.Aplication.Commands;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class PatchPlanHandler : IRequestHandler<PatchPlanCommand>
    {
        public Task Handle(PatchPlanCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}