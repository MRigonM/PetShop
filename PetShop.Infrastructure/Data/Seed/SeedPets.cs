using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;
using PetShop.Domain.Enums;

namespace PetShop.Infrastructure.Data.Seed;

public class SeedPets
{
    public static async Task SeedPetData(AppDbContext context)
    {
        var petExists = await context.Pets.AnyAsync();
        if (petExists) return;
        
        var userAsd = await context.Users.FirstOrDefaultAsync(u => u.Email == "asd@qwe.com");
        var userBob = await context.Users.FirstOrDefaultAsync(u => u.Email == "bob@example.com");

        var pets = new List<Pet>
        {
            new Pet
            {
                Name = "Buddy",
                BreedId = 1, // Golden Retriever
                AgeYears = "3-7 Years",
                About =
                    "Buddy is the perfect adventure buddy! Whether it's a long walk in the park or just chilling at home, he’s always there to make you smile with his playful and loyal personality. Loves belly rubs and fetch!",
                Status = PetStatus.Approved,
                LocationId = 1, // Pristina
                PostedByUserId = userAsd.Id
            },
            new Pet
            {
                Name = "Max",
                BreedId = 2, // Labrador Retriever
                AgeYears = "0-3 Months",
                About = "Max is a little bundle of joy who’s still learning his way around the world! He’s full of energy and loves meeting new people. If you’re looking for a loyal and goofy companion, Max is your guy!",
                Status = PetStatus.Approved,
                LocationId = 6, // Tirana
                PostedByUserId = userAsd.Id
            },
            new Pet
            {
                Name = "Whiskers",
                BreedId = 4, // Persian
                AgeYears = "3-7 Years",
                About = "Whiskers is the ultimate lap cat! With a fluffy coat, he's the perfect companion for those quiet, cozy nights. He enjoys soft purring naps and watching the world go by from the window.",
                Status = PetStatus.Available,
                LocationId = 3, // Mitrovica
                PostedByUserId = userBob.Id
            },
        };
        
        context.Pets.AddRange(pets);
        await context.SaveChangesAsync();
    }
}