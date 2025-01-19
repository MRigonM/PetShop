using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data.Seed;

namespace PetShop.Infrastructure.Data;

public class DataSeeder
{
    public static async Task SeedData(AppDbContext context, IUnitOfWork unitOfWork)
    {
        await SeedPets.SeedPetData(context);
    }
}