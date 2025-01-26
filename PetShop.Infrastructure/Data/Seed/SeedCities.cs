using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Data.Seed;

public static class SeedCities
{
    public static async Task SeedCityData(AppDbContext context)
    {
        var citiesExist = await context.Cities.AnyAsync();
        if (citiesExist) return;

        var cities = new List<City>
        {
            new City
            {
                Name = "Prishtinë",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Prizren",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            },
            new City
            {
                Name = "Mitrovicë",
                CountryId = 1,
                IsDeleted = false,
                DeletedAt = null
            }
        };

        context.Cities.AddRange(cities);
        await context.SaveChangesAsync();
    }
}