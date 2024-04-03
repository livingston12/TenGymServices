using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Products.Core.Dtos;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.Shared.Core.Extentions;

namespace TenGymServices.Api.Products.Aplication.Products
{
    public class Patch
    {
        public class UpdateProduct : IRequest
        {
            public int ProductId { get; set; }
            public JsonPatchDocument<ProductPatchDTO> PatchDocument { get; set; }
        }

        public class UpdateProductHandler : IRequestHandler<UpdateProduct>
        {
            private readonly ProductContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccesor;

            public UpdateProductHandler(ProductContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(UpdateProduct request, CancellationToken cancellationToken)
            {
                if (request.PatchDocument == null)
                {
                    request.ThrowHttpHandlerExeption("Please verify your body request",  HttpStatusCode.BadRequest);
                }

                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == request.ProductId);

                if (product == null)
                {
                    request.ThrowHttpHandlerExeption("Product does not exist",  HttpStatusCode.NotFound);
                }

                var productDto = _mapper.Map<ProductPatchDTO>(product);
                request.PatchDocument.ApplyTo(productDto);

                _mapper.Map(productDto, product);
                await _context.SaveChangesAsync();
            }
        }
    }
}