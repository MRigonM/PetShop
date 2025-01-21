using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class SpeciesRepository : GenericRepository<Species, int>, ISpeciesRepository
{
    public SpeciesRepository(AppDbContext context) : base(context)
    {
    }
}