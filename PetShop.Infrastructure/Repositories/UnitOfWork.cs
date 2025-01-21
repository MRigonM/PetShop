using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}