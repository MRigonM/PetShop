﻿using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    protected readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(AppDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync([id]);
    }
    
    public async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public async Task<TKey> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity.Id;
    }
    
    public Task UpdateAsync(TEntity entity)
    {
        return Task.FromResult(_dbSet.Update(entity));
    }
    
    public Task DeleteAsync(TEntity entity)
    {
        return Task.FromResult(_dbSet.Remove(entity));
    }
}