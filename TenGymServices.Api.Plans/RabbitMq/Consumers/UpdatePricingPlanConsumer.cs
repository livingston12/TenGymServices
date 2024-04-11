using System.Net;
using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Plans.Core.Dtos;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Api.Plans.RabbitMq.Queues;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Persistence;

namespace TenGymServices.Api.Plans.RabbitMq.Consumers
{
    public class UpdatePricingPlanConsumer : IConsumer<UpdatePricingPlanQuee>
    {
        private readonly IMapper _mapper;
        private readonly PlanContext _context;
        private readonly IRepositoryDB _planRepository;

        public UpdatePricingPlanConsumer(
            IMapper mapper,
            PlanContext context,
            IRepositoryDB planRepository)
        {
            _mapper = mapper;
            _context = context;
            _planRepository = planRepository;
        }

        public async Task Consume(ConsumeContext<UpdatePricingPlanQuee> context)
        {
            await UpdatePricingPlanDb(context.Message);
        }

        private async Task UpdatePricingPlanDb(UpdatePricingPlanQuee request)
        {
            var fixedPricesEntity = _context.FixedPrices?
                .Where(x => x.PricingScheme.BillingCycle.PlanId == request.PlanId)
                .Include(x => x.PricingScheme)
                .ToList();

            if (fixedPricesEntity == null)
            {
                return;
            }

            var dictonaryRequest = request.ListPricingPlans.ToDictionary(x => x.BillingCycleSequence, x => new {
                x.PricingScheme.FixedPrice.CurrencyCode,
                x.PricingScheme.FixedPrice.Value
            });
            int counter = 1;
            foreach (var pricing in fixedPricesEntity) 
            {
                pricing.CurrencyCode = dictonaryRequest[counter].CurrencyCode;
                pricing.Value = dictonaryRequest[counter].Value;
                counter++;
            }
            
            var inserted = await _planRepository.Commit(_context);
            if (inserted == 0)
            {
                request.ThrowHttpHandlerExeption("Cannot update pricing", HttpStatusCode.BadRequest);
            }
        }
    }
}