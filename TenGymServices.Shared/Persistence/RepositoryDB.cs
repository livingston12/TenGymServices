using Microsoft.EntityFrameworkCore;

namespace TenGymServices.Shared.Persistence
{
    public class RepositoryDB : IRepositoryDB

    {
        public async Task AddAsync<TContext, TEntity>(TContext dbContext, TEntity entity)
            where TContext : DbContext
            where TEntity : class
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<int> Commit<TContext>(TContext dbContext) where TContext : DbContext
        {
            return await dbContext.SaveChangesAsync();
        }

        public void UpdateAsync<TContext, TEntity>(TContext dbContext, TEntity entity)
            where TContext : DbContext
            where TEntity : class
        {
            
            dbContext.Set<TEntity>().Update(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}