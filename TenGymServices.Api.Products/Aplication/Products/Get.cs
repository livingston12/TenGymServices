using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Products.Models.Dtos;
using TenGymServices.Api.Products.Models.Entities;
using TenGymServices.Api.Products.Persistence;

namespace TenGymServices.Api.Products.Aplication.Products
{
    public class Get
    {
        public class GetProducts : IRequest<List<ProductDto>>
        {
        }

        // Request product By Id
        public class GetProductsById : IRequest<ProductDto>
        {
            public int ProductId { get; set; }
        }

        public class GetProductsHandler : IRequestHandler<GetProducts, List<ProductDto>>
        {
            private readonly ProductContext _context;
            private readonly IMapper _mapper;

            public GetProductsHandler(ProductContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            // Logic get all products
            public async Task<List<ProductDto>> Handle(GetProducts request, CancellationToken cancellationToken)
            {
                var list = await _context.Products.ToListAsync();
                var productDto = _mapper.Map<List<ProductsEntity>, List<ProductDto>>(list);

                return productDto;
            }
        }

        // Logic product By Id
        public class GetProductsByIdHandler : IRequestHandler<GetProductsById, ProductDto>
        {
            private readonly ProductContext _context;
            private readonly IMapper _mapper;

            public GetProductsByIdHandler(ProductContext context, IMapper mapper) 
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(GetProductsById request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                                            .FirstOrDefaultAsync(x => x.ProductId == request.ProductId);
                if (product == null)
                {
                    throw new Exception("Product does not exist");
                }
                var productDto = _mapper.Map<ProductsEntity, ProductDto>(product);

                return productDto;
            }
        }
    }
}