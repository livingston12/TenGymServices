using System.Net.Http.Headers;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Products.Aplication.ExternalServices;
using TenGymServices.Api.Products.Core.Interfaces;
using TenGymServices.Api.Products.Core.Utils;
using TenGymServices.Api.Products.HandlerRabiitmq;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.RabbitMq.Bus.BusRabbit;
using TenGymServices.RabbitMq.Bus.EventQuees;
using TenGymServices.RabbitMq.Bus.Implements;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Implements;

namespace TenGymServices.Api.Products
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
            .AddNewtonsoftJson();

            services.AddAutoMapper(typeof(MapperProfiles));

            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ProductContext>(cfg =>
                cfg.UseSqlServer(configuration.GetConnectionString("ProductDatabase")));


            // Paypal client Http
            services.AddHttpClient("PaypalClient", opt =>
            {
                opt.BaseAddress = new Uri(configuration["Services:BasePaypalUrl"].ToString());
            })
            .ConfigureHttpClient(async (services, client) =>
            {
                client.DefaultRequestHeaders.Add("PayPal-Request-Id", Guid.NewGuid().ToString());
                client.DefaultRequestHeaders.Add("Prefer", "return=representation");

                // Generate Token
                var serviceProvider = services.GetRequiredService<IServiceProvider>();
                var paypalService = serviceProvider.GetRequiredService<IPaypalAuthService>();
                string token = await paypalService.GenerateToken(client, configuration["Paypal:ClientId"], configuration["Paypal:SecretId"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddScoped<IPaypalProductService, PaypalService>();
            services.AddSingleton<ExceptionHandlerMiddleware>();
            services.AddSingleton<IPaypalAuthService, PaypalAuthService>();
            services.AddSingleton<IRabbitEventBus, RabbitEventBus>(x =>
            {
                var mediator = x.GetService<IMediator>();
                var logger = x.GetService<ILogger<RabbitEventBus>>();
                var scopeFactory = x.GetService<IServiceScopeFactory>();
                var rabbit = new RabbitEventBus(mediator, scopeFactory)
                {
                    HostName = "TenGym.Rabbitmq-web"
                };
                return rabbit;
            });

            services.AddTransient<ProductEventHandler>();

            // Consume Rabbitmq
            services.AddTransient<IEventHandler<ProductEventQuee>, ProductEventHandler>();

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            try
            {
                var context = app.Services.GetRequiredService<ProductContext>();
                app.MigrateDatabase<ProductContext>();
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "Migration Failed");
            }
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // consume rabbitmq
            var eventBus = app.Services.GetRequiredService<IRabbitEventBus>();
            //eventBus.HostName = "TenGym.Rabbitmq-web";
            //eventBus.Exchange = "TenGym.Product";
            eventBus.Suscribe<ProductEventQuee, ProductEventHandler>();

        }
    }
}