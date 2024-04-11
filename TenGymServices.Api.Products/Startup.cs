using System.Net.Http.Headers;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TenGymServices.Api.Products.Aplication.ExternalServices;
using TenGymServices.Api.Products.Core.Interfaces;
using TenGymServices.Api.Products.Core.Utils;
using TenGymServices.Api.Products.Persistence;
using TenGymServices.Api.Products.RabbitMq.Consumers;
using TenGymServices.RabbitMq.Bus.IBusMassTransient;
using TenGymServices.RabbitMq.Bus.Implements;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Interfaces;
using TenGymServices.Shared.Implements;
using TenGymServices.Shared.Persistence;

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
            services.AddSwaggerGen();
             services.AddSwaggerGenNewtonsoftSupport();

            // Add Fluent Validation
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddControllers().AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();

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
            });
            services.AddAutoMapper(typeof(MapperProfiles));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddServicesLifeCycles();


            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<CreateProductConsumer>();
                x.AddConsumer<PatchProductConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:HostName"].ToString(), "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });

            });

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
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



        }
    }
}