using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Data.Seed;

public static class SeedBreeds
{
    public static async Task SeedBreedData(AppDbContext context)
    {
        var breedsExists = await context.Breeds.AnyAsync();
        if (breedsExists) return;

        var breeds = new List<Breed>
        {
            new()
            {
                SpeciesId = 1, // Dog
                Name = "Golden Retriever"
            },
            new()
            {
                SpeciesId = 1, // Dog
                Name = "Labrador Retriever"
            },
            new()
            {
                SpeciesId = 1, // Dog
                Name = "German Shepherd"
            },

            // Cat Breeds
            new()
            {
                SpeciesId = 2, // Cat
                Name = "Persian"
            },
            new()
            {
                SpeciesId = 2, // Cat
                Name = "Siamese"
            },
            new()
            {
                SpeciesId = 2, // Cat
                Name = "Maine Coon"
            }
        };

        context.Breeds.AddRange(breeds);
        await context.SaveChangesAsync();
    }
}