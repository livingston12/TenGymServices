using AutoMapper;
using FluentValidation;
using MediatR;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Requests;
using TenGymServices.Api.Products.Core.Interfaces;
using System.Net;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;
using TenGymServices.Api.Products.RabbitMq.Queues;

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
            private readonly IMapper _mapper;
            private readonly IPaypalProductService<ProductPaypalRequest> _paypalService;
            private readonly IMassTransientBus _rabbitEventBus;

            public CreateTaskCommandHandler(
                IMapper mapper,
                IPaypalProductService<ProductPaypalRequest> paypalService,
                IMassTransientBus rabbitEventBus
                )
            {
                _mapper = mapper;
                _paypalService = paypalService;
                _rabbitEventBus = rabbitEventBus;
            }

            public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                request.ValidateRequest<CreateTaskCommand, CreateTaskCommandValidator>();

                var requestPaypal = _mapper.Map<CreateTaskCommand, ProductPaypalRequest>(request);

                var responsePaypal = await _paypalService.PostAsync(requestPaypal, "/v1/catalogs/products");
                if (responsePaypal.hasEerror)
                {
                    request.ThrowHttpHandlerExeption("Product does not exist", HttpStatusCode.BadRequest);
                }
                requestPaypal.paypal_id = responsePaypal.Id;
                var productQuee = _mapper.Map<ProductPaypalRequest, ProductEventQuee>(requestPaypal);
                _rabbitEventBus.Publish(productQuee);
            }



        }


    }
}