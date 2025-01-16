using PetShop.Domain.Entities;
using PetShop.Domain.Helpers;

namespace PetShop.Domain.Interfaces;

public interface IPetRepository : IGenericRepository<Pet, int>
{
    Task<(List<Pet> Pets, int TotalCount)> GetAvailablePetsWithDetailsAsync(QueryParams queryParams, CancellationToken cancellationToken);
    Task<IEnumerable<Pet>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, CancellationToken cancellationToken = default);
    Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId, CancellationToken cancellationToken);
}