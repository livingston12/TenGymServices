using Azure;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TenGymServices.Api.Products.Aplication;
using TenGymServices.Api.Products.Aplication.Products;
using TenGymServices.Api.Products.Core.Dtos;
using TenGymServices.Api.Products.Models.Dtos;

namespace TenGymServices.Api.Products.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Post([FromBody] Post.CreateTaskCommand request)
        {
            await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAll()
        {
            Get.GetProducts request = new Get.GetProducts();
            return await _mediator.Send(request, new CancellationToken());
        }

        [HttpGet("{ProductId:int}")]
        public async Task<ProductDto> GetById([FromRoute] Get.GetProductsById request)
        {
            return await _mediator.Send(request, new CancellationToken());
        }

        [HttpPatch("{ProductId:int}")]
        public async Task UpdateProduct([FromRoute] int ProductId, [FromBody] JsonPatchDocument<ProductPatchDTO> patchDocument)
        {
            Patch.UpdateProduct path = new Patch.UpdateProduct()
            {
                PatchDocument = patchDocument,
                ProductId = ProductId
            };
            await _mediator.Send(path, new CancellationToken());
        }
    }
}