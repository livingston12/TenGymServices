using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TenGymServices.Api.Plans.Core.Filters;
using TenGymServices.Api.Plans.Persistence;
using TenGymServices.Shared.Core.Extentions;

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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddDbContext<PlanContext>(cfg =>
            {
                cfg.UseSqlServer(_configuration.GetConnectionString("PlanDatabase"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TenGymServices Plans", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
            });
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
        }
    }
}