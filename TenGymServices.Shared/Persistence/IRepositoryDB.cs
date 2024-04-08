using Microsoft.EntityFrameworkCore;

namespace TenGymServices.Shared.Persistence;

public interface IRepositoryDB
{
    Task<int> AddAsync<TContext, TEntity>(TContext dbContext, TEntity entity) 
        where TEntity : class
        where TContext : DbContext;
}
