using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Data.Seed;

public static class SeedSpecies
{
    public static async Task SeedSpeciesData(AppDbContext context)
    {
        var speciesExist = await context.Species.AnyAsync();
        if (speciesExist) return;

        var species = new List<Species>
        {
            new()
            {
                Name = "Dog",
            },
            new()
            {
                Name = "Cat",
            }
        };

        context.Species.AddRange(species);
        await context.SaveChangesAsync();
    }
}