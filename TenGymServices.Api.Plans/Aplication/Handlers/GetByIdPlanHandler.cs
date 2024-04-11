using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Shared.Core.Extentions;

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
            var plan = await _context.Plans.FirstOrDefaultAsync(plan => plan.PlanId == request.PlanId, cancellationToken);
            if (plan == null)
            {
                request.ThrowHttpHandlerExeption("Plan does not exist", HttpStatusCode.BadRequest);
            }
            var planDto = _mapper.Map<PlanEntity, PlanDto>(plan);

            return planDto;
        }
    }
}