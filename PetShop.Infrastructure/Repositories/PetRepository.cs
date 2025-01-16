using PetShop.Domain.Entities;
using PetShop.Domain.Helpers;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class PetRepository : GenericRepository<Pet, int>, IPetRepository
{
    public PetRepository(AppDbContext context) : base(context)
    {
    }

    public Task<(List<Pet> Pets, int TotalCount)> GetAvailablePetsWithDetailsAsync(QueryParams queryParams, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}