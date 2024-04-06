using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.Persistence;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public class GetByIdPlanHandler : IRequestHandler<GetByIdPlanQuery, PlanDto>
    {
        private readonly PlanContext _context;
        private readonly IMapper _mapper;
        public GetByIdPlanHandler(PlanContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<PlanDto> Handle(GetByIdPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await _context.Plans.FirstOrDefaultAsync(plan => plan.PlanId == request.planId, cancellationToken);
            if (plan == null)
            {
                throw new Exception("Plan does not exist");
            }
            var planDto = _mapper.Map<PlanEntity, PlanDto>(plan);

            return planDto;
        }
    }
}