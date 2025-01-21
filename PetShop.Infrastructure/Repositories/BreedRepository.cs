using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class BreedRepository : GenericRepository<Breed, int>, IBreedRepository
{
    public BreedRepository(AppDbContext context) : base(context)
    {
    }
}