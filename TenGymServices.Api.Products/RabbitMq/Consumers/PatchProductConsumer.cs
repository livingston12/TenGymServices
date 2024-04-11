using System.Net;
using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using TenGymServices.Api.Products.Core.Dtos;
using TenGymServices.Api.Products.Models.Entities;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.Api.Products.RabbitMq.Queues;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Persistence;
using static TenGymServices.Api.Products.Aplication.Products.Get;

namespace TenGymServices.Api.Products.RabbitMq.Consumers
{
    public class PatchProductConsumer : IConsumer<PatchProductQuee>
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly IRepositoryDB _repositoryDB;

        public PatchProductConsumer(
            ProductContext context,
            IMapper mapper,
            IRepositoryDB repositoryDB)
        {
            _context = context;
            _mapper = mapper;
            _repositoryDB = repositoryDB;
        }
        public async Task Consume(ConsumeContext<PatchProductQuee> context)
        {
            await UpdateProductDB(context.Message);
        }

        // updates a product into the database based on the given ProductEventQuee request.
        private async Task UpdateProductDB(PatchProductQuee request)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == request.ProductId);
            var tt = _mapper.Map(request.PatchProduct, product );
            _repositoryDB.UpdateAsync(_context, product);
            //await _context.SaveChangesAsync();
           
            
            var inserted = await _repositoryDB.Commit(_context);

            if (inserted == 0)
            {
                request.ThrowHttpHandlerExeption("Cannot update the the product", HttpStatusCode.BadRequest);
            }
        }
    }
}