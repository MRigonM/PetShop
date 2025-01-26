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
                About = "Buddy is the perfect adventure buddy! Whether it's a long walk in the park or just chilling at home, he’s always there to make you smile with his playful and loyal personality. Loves belly rubs and fetch!",
                Status = PetStatus.Approved,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1, // Pristina
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Max",
                BreedId = 2, // Labrador Retriever
                AgeYears = "0-3 Months",
                About = "Max is a little bundle of joy who’s still learning his way around the world! He’s full of energy and loves meeting new people. If you’re looking for a loyal and goofy companion, Max is your guy!",
                Status = PetStatus.Approved,
                CreatedAt = DateTime.UtcNow,
                LocationId = 2, // Mitro
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Whiskers",
                BreedId = 4, // Persian
                AgeYears = "3-7 Years",
                About = "Whiskers is the ultimate lap cat! With a fluffy coat, he's the perfect companion for those quiet, cozy nights. He enjoys soft purring naps and watching the world go by from the window.",
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 2, // Mitrovica
                PostedByUserId = userAsd.Id,
                IsDeleted = false
            },
            // Pets assigned to userBob (1 dog, 2 cats)
            new Pet
            {
                Name = "Rocky",
                BreedId = 2, // Beagle
                AgeYears = "3-6 Months",
                About = "Rocky is an energetic, curious little pup who loves to explore everything! Whether he's sniffing out new trails or playing in the yard, he’s always on the move. Perfect for active families who love the outdoors!",
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 1, // Prishtina
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Mittens",
                BreedId = 5, // Siamese
                AgeYears = "3-6 Months",
                About = "Mittens is a talkative and social little furball who loves to chat with her humans! She’s always curious and loves to be part of the action. With her striking blue eyes, she’ll steal your heart in no time!",
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 2, // Mitro
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
            new Pet
            {
                Name = "Shadow",
                BreedId = 6, // Maine Coon
                AgeYears = "3-7 Years",
                About = "Shadow is a gentle giant with a heart full of love! Despite his size, he's a calm and affectionate cat who enjoys cuddling up by your side. He’s independent but always down for some quality petting time.",
                Status = PetStatus.Available,
                CreatedAt = DateTime.UtcNow,
                LocationId = 2, // Mitro
                PostedByUserId = userBob.Id,
                IsDeleted = false
            },
        };
        
        context.Pets.AddRange(pets);
        await context.SaveChangesAsync();
    }
}