using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Data.Seed;

public static class SeedLocations
{
    public static async Task SeedLocationData(AppDbContext context)
    {
        var locationsExist = await context.Locations.AnyAsync();
        if (locationsExist) return;

        var locations = new List<Location>
        {
            new Location
            {
                CityId = 1, // Pristina
                Address = "Bulevardi Nënë Tereza 1",
                PostalCode = "10000",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 2, // Prizren
                Address = "Rruga e Shadërvanit 15",
                PostalCode = "20000",
                IsDeleted = false,
                DeletedAt = null
            },
            new Location
            {
                CityId = 3, // Mitrovica
                Address = "Rruga Adem Jashari 9",
                PostalCode = "40000",
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Locations.AddRange(locations);
        await context.SaveChangesAsync();
    }
}