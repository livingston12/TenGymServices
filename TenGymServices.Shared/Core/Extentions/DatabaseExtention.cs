using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TenGymServices.Shared.Core.Extentions
{
    public static class DatabaseExtention
    {
        
        public static void MigrateDatabase<TContext>(this WebApplication app)
            where TContext : DbContext
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<TContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}