using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class LocationRepository: GenericRepository<Location, int>, ILocationRepository
{
    public LocationRepository(AppDbContext context) : base(context)
    {
    }
}