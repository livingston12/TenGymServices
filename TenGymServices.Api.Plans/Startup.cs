using Microsoft.OpenApi.Models;
using TenGymServices.Api.Plans.Core.Filters;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TenGymServices Plans", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                      "WebApiDevoTo v1"));

            app.UseReDoc();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}