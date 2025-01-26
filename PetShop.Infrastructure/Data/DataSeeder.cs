using Microsoft.AspNetCore.Identity;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data.Seed;

namespace PetShop.Infrastructure.Data;

public class DataSeeder
{
    public static async Task SeedData(AppDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        await SeedUsers.SeedUserData(userManager);
        await SeedSpecies.SeedSpeciesData(context);
        await SeedBreeds.SeedBreedData(context);
        await SeedCountries.SeedCountryData(context);
        await SeedCities.SeedCityData(context);
        await SeedLocations.SeedLocationData(context);
        await SeedPets.SeedPetData(context);
        await SeedPetImages.SeedPetImageData(context);
    }
}