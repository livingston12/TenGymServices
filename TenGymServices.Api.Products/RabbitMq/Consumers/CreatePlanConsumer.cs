using System.Net;
using AutoMapper;
using MassTransit;
using TenGymServices.Api.Products.Models.Entities;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.Api.Products.RabbitMq.Queues;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Products.RabbitMq.Consumers
{
    public class CreatePlanConsumer : IConsumer<ProductEventQuee>
    {
        private readonly IMapper _mapper;
        private readonly ProductContext _context;
        public CreatePlanConsumer(
            IMapper mapper,
            ProductContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProductEventQuee> context)
        {
            await InsertProductDB(context.Message);
        }
        
        // Inserts a product into the database based on the given ProductEventQuee request.
        private async Task InsertProductDB(ProductEventQuee request)
        {
            var entity = _mapper.Map<ProductEventQuee, ProductsEntity>(request);

            await _context.Products.AddAsync(entity);
            var inserted = await _context.SaveChangesAsync();

            if (inserted == 0)
            {
                request.ThrowHttpHandlerExeption("Cannot insert the the product", HttpStatusCode.BadRequest);
            }
        }

    }
}