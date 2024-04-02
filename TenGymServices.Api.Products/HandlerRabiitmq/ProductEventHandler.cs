using AutoMapper;
using TenGymServices.Api.Products.Models.Dtos;
using TenGymServices.Api.Products.Models.Entities;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.EventQuees;

namespace TenGymServices.Api.Products.HandlerRabiitmq
{
    public class ProductEventHandler : IEventHandler<ProductEventQuee>
    {
        private readonly IMapper _mapper;
        private readonly ProductContext _context;
        private readonly ILogger<ProductEventHandler> _logger;
        public ProductEventHandler(
            ILogger<ProductEventHandler> logger,
            IMapper mapper,
            ProductContext context)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ProductEventQuee @event)
        {
            try
            {
                //_logger.LogInformation("Handle event");
                // Save product in database
                await InsertProductDB(@event);
                //return Task.CompletedTask;
            }
            catch (System.Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

        }

        private async Task<ProductDto> InsertProductDB(ProductEventQuee request)
        {
            var entity = _mapper.Map<ProductEventQuee, ProductsEntity>(request);

            await _context.Products.AddAsync(entity);
            var inserted = await _context.SaveChangesAsync();

            if (inserted > 0)
            {
                var dto = _mapper.Map<ProductsEntity, ProductDto>(entity);
                return dto;
            }
            else
            {
                throw new Exception("Cannot insert the the product");
            }
        }

    }
}