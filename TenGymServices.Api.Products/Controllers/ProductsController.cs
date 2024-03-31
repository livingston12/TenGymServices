using MediatR;
using Microsoft.AspNetCore.Mvc;
using TenGymServices.Api.Products.Aplication;
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
        public async Task Post(Post.CreateTaskCommand request)
        {
            await _mediator.Send(request, new CancellationToken());
        }
    }
}