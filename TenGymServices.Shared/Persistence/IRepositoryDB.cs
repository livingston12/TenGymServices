using Microsoft.EntityFrameworkCore;

namespace TenGymServices.Shared.Persistence;

public interface IRepositoryDB
{
    Task AddAsync<TContext, TEntity>(TContext dbContext, TEntity entity)
        where TEntity : class
        where TContext : DbContext;
    void UpdateAsync<TContext, TEntity>(TContext dbContext, TEntity entity)
        where TEntity : class
        where TContext : DbContext;
    
    Task<int> Commit<TContext>(TContext dbContext)
        where TContext : DbContext;
}
