using MassTransit;
using TenGymServices.Api.Products.Aplication.ExternalServices;
using TenGymServices.Api.Products.Core.Interfaces;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;
using TenGymServices.RabbitMq.Bus.Implements;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Implements;
using TenGymServices.Shared.Persistence;

namespace TenGymServices.Api.Products.Core.Utils
{
    public static class ServicesLifeCyclesExtentions
    {
        public static void AddServicesLifeCycles(this IServiceCollection services)
        {

            services.AddSingleton<ExceptionHandlerMiddleware>();
            services.AddSingleton<IPaypalAuthService, PaypalAuthService>();
            services.AddScoped(typeof(IPaypalProductService<>), typeof(PaypalService<>));
            services.AddScoped<IRepositoryDB, RepositoryDB>();
            services.AddSingleton<IMassTransientBus, MassTransientBus>(x =>
            {
                var publishEndpoint = x.GetService<IPublishEndpoint>();
                return new MassTransientBus(publishEndpoint);
            });
        }
    }
}