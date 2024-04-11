using MassTransit;
using TenGymServices.Api.Plans.Aplication.ExternalServices;
using TenGymServices.Api.Plans.Core.Entities;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.Implements;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Implements;
using TenGymServices.Shared.Persistence;

namespace TenGymServices.Api.Plans.Core.Utils
{
    public static class ServicesLifeCyclesExtension
    {
        public static void AddServicesLifeCycles(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionHandlerMiddleware>(); // Manage Exception
            services.AddSingleton<IPaypalAuthService, PaypalAuthService>();

            services.AddScoped(typeof(IPaypalPlansService<>), typeof(PaypalService<>));
            //services.AddScoped(typeof(IRepositoryDB<>), typeof(RepositoryDB<>));

            services.AddScoped<IRepositoryDB, RepositoryDB>();

            services.AddSingleton<IMassTransientBus, MassTransientBus>(x =>
            {
                var publishEndpoint = x.GetService<IPublishEndpoint>();
                return new MassTransientBus(publishEndpoint);
            });
        }
    }
}