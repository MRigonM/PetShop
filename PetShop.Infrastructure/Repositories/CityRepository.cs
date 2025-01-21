using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class CityRepository : GenericRepository<City, int>, ICityRepository
{
    public CityRepository(AppDbContext context) : base(context)
    {
    }
}