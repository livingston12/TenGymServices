using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Aplication.Queries;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.Persistence;

namespace TenGymServices.Api.Plans.Aplication.Handlers
{
    public record GetAllPlanHandler : IRequestHandler<GetAllPlanQuery, List<PlanDto>>
    {
        private readonly PlanContext _context;
        private readonly IMapper _mapper;
        public GetAllPlanHandler(PlanContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PlanDto>> Handle(GetAllPlanQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Plans.ToListAsync(cancellationToken);
            var productDto = _mapper.Map<List<PlanEntity>, List<PlanDto>>(list);

            return productDto;
        }
    }
}