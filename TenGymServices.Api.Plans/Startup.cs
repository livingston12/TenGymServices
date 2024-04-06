using System.Net.Http.Headers;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TenGymServices.Api.Plans.Core.Filters;
using TenGymServices.Api.Plans.Core.Utils;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Interfaces;

namespace TenGymServices.Api.Plans
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();;
            services.AddEndpointsApiExplorer();
            
            // Add Fluent Validation
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
                
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TenGymServices Plans", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
            });

            services.AddDbContext<PlanContext>(cfg =>
            {
                cfg.UseSqlServer(_configuration.GetConnectionString("PlanDatabase"));
            });

            // Add http client to consumer paypal
            services.AddHttpClient("PaypalClient", opt =>
          {
              opt.BaseAddress = new Uri(_configuration["Services:BasePaypalUrl"].ToString());
          })
          .ConfigureHttpClient(async (services, client) =>
          {
              client.DefaultRequestHeaders.Add("PayPal-Request-Id", Guid.NewGuid().ToString());
              client.DefaultRequestHeaders.Add("Prefer", "return=representation");

              // Get Token for each request
              var serviceProvider = services.GetRequiredService<IServiceProvider>();
              var paypalService = serviceProvider.GetRequiredService<IPaypalAuthService>();
              string token = await paypalService.GenerateToken(client, _configuration["Paypal:ClientId"], _configuration["Paypal:SecretId"]);
              client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          });

            services.AddAutoMapper(typeof(MapperProfiles));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly)); // Add MediaTR 
            services.AddServicesLifeCycles();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            try
            {
                var context = app.Services.GetRequiredService<PlanContext>();
                app.MigrateDatabase<PlanContext>();
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "Migration Failed");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                      "TenGymServices Plans v1"));

            app.UseReDoc();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<ExceptionHandlerMiddleware>(); // Use manage Exception
        }
    }
}