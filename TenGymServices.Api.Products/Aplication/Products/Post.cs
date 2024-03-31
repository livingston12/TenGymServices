using AutoMapper;
using FluentValidation;
using MediatR;
using TenGymServices.Api.Products.Models.Dtos;
using TenGymServices.Api.Products.Models.Entities;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.Api.Products.Services;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Requests;
using Microsoft.Extensions.DependencyInjection;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.EventQuees;

namespace TenGymServices.Api.Products.Aplication
{
    public class Post
    {
        public class CreateTaskCommand : IRequest
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? ImageUrl { get; set; }
            public string? HomeUrl { get; set; }
        }

        public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
        {
            public CreateTaskCommandValidator()
            {
                RuleFor(x => x.Name)
                     .NotEmpty()
                     .Length(3, 127);
                RuleFor(x => x.Description)
                    .Length(0, 256);
                RuleFor(x => x.ImageUrl)
                    .Length(0, 256);
                RuleFor(x => x.HomeUrl)
                    .Length(0, 256);
            }
        }

        public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
        {
            private readonly ProductContext _context;
            private readonly IMapper _mapper;
            private readonly IPaypalProductService _paypalService;
            private readonly IRabbitEventBus _rabbitEventBus;

            public CreateTaskCommandHandler(
                ProductContext context,
                IMapper mapper,
                IPaypalProductService paypalService,
                IRabbitEventBus rabbitEventBus
                )
            {
                _context = context;
                _mapper = mapper;
                _paypalService = paypalService;
                _rabbitEventBus = rabbitEventBus;
            }

            public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                request.ValidateRequest<CreateTaskCommand, CreateTaskCommandValidator>();

                var requestPaypal = _mapper.Map<CreateTaskCommand, ProductPaypalRequest>(request);

                // Call Paypal and insert the product

                var responsePaypal = await _paypalService.CreateProduct(requestPaypal);
                if (responsePaypal.hasEerror)
                {
                    throw new Exception(responsePaypal.MessageError);

                }
                requestPaypal.paypal_id = responsePaypal.ProductId;
                // TODO: Configure send responsePaypal by rabbitMQ 
                var productQuee = _mapper.Map<ProductPaypalRequest, ProductEventQuee>(requestPaypal);
                
                _rabbitEventBus.Publish(productQuee);
                //return await InsertProductDB(request, responsePaypal);
            }

            private async Task<ProductDto> InsertProductDB(CreateTaskCommand request, (bool hasEerror, string ProductId, string MessageError) responsePaypal)
            {
                if (responsePaypal.hasEerror == false)
                {
                    var entity = _mapper.Map<CreateTaskCommand, ProductsEntity>(request);
                    entity.PaypalId = responsePaypal.ProductId;

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


                throw new Exception(responsePaypal.MessageError);
            }


        }


    }
}