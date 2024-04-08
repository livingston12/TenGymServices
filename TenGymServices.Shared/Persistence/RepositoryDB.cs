using Microsoft.EntityFrameworkCore;

namespace TenGymServices.Shared.Persistence
{
    public class RepositoryDB : IRepositoryDB

    {
        public async Task<int> AddAsync<TContext, TEntity>(TContext dbContext, TEntity entity)
            where TContext : DbContext
            where TEntity : class
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}