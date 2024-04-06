using TenGymServices.Api.Plans.Aplication.ExternalServices;
using TenGymServices.Api.Plans.Core.Interfaces;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.Implements;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Implements;

namespace TenGymServices.Api.Plans.Core.Utils
{
    public static class ServicesLifeCyclesExtension
    {
        public static void AddServicesLifeCycles(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionHandlerMiddleware>(); // Manage Exception
            services.AddSingleton<IPaypalAuthService, PaypalAuthService>();

            services.AddScoped(typeof(IPaypalPlansService<>), typeof(PaypalService<>));

            services.AddSingleton<IRabbitEventBus, RabbitEventBus>();
        }
    }
}