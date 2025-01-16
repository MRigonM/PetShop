using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Interfaces;

namespace PetShop.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    protected readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(AppDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }
}