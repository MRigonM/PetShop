using PetShop.Domain.Entities;
using PetShop.Domain.Helpers;

namespace PetShop.Domain.Interfaces;

public interface IPetRepository : IGenericRepository<Pet, int>
{
    Task<(List<Pet> Pets, int TotalCount)> GetAvailablePetsWithDetailsAsync(QueryParams queryParams);
    Task<IEnumerable<Pet>> GetByUserIdAsync(string userId);
    Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id);
    Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, string userId);
    Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId);
}