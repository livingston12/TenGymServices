using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using TenGymServices.Api.Products.Core.Dtos;
using TenGymServices.Api.Products.Core.Interfaces;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.Api.Products.RabbitMq.Queues;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;
using TenGymServices.Shared.Core.Extentions;
using static TenGymServices.Api.Products.Aplication.Products.Get;

namespace TenGymServices.Api.Products.Aplication.Products
{
    public class Patch
    {
        public class UpdateProduct : IRequest
        {
            public int ProductId { get; set; }
            public JsonPatchDocument<ProductPatchDto> PatchDocument { get; set; }
        }

        public class UpdateProductHandler : IRequestHandler<UpdateProduct>
        {
            private readonly ProductContext _context;
            private readonly IMapper _mapper;
            private readonly IMassTransientBus _massTransientBus;
            private readonly IPaypalProductService<JsonPatchDocument<ProductPatchDto>> _paypalService;
            private readonly IMediator _mediator;

            public UpdateProductHandler(
                ProductContext context,
                IMapper mapper,
                IMassTransientBus massTransientBus,
                IPaypalProductService<JsonPatchDocument<ProductPatchDto>> paypalService,
                IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _massTransientBus = massTransientBus;
                _mediator = mediator;
                _paypalService = paypalService;
            }

            public async Task Handle(UpdateProduct request, CancellationToken cancellationToken)
            {
                if (request.PatchDocument == null)
                {
                    request.ThrowHttpHandlerExeption("Please verify your body request", HttpStatusCode.BadRequest);
                }

                var product = await _mediator.Send(new GetProductsById() { ProductId = request.ProductId }, cancellationToken);

                var PatchPlanPaypal = await _paypalService.PostAsync(request.PatchDocument, $"/v1/catalogs/products/{product.PaypalId}", HttpMethod.Patch);

                if (PatchPlanPaypal.hasEerror)
                {
                    request.ThrowHttpHandlerExeption(PatchPlanPaypal.MessageError, HttpStatusCode.BadRequest);
                }

                var patchProductDto = _mapper.Map<ProductPatchDto>(product);
                request.PatchDocument.ApplyTo(patchProductDto);

                var planQuee = new PatchProductQuee()
                {
                    ProductId = request.ProductId,
                    PatchProduct = patchProductDto
                };

                _massTransientBus.Publish(planQuee);
            }
        }
    }
}