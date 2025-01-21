using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class PetImageRepository : GenericRepository<PetImage, int>, IPetImageRepository
{
    public PetImageRepository(AppDbContext context) : base(context)
    { }
}