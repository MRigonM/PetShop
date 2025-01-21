using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class CountryRepository : GenericRepository<Country, int>, ICountryRepository
{
    public CountryRepository(AppDbContext context) : base(context)
    {
    }
}
