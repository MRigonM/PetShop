using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task UseDataSeeder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting database migration and data seeding.");

            var context = services.GetRequiredService<AppDbContext>();
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migration completed.");
            }
            else
            {
                logger.LogInformation("No pending migrations.");
            }

            await DataSeeder.SeedData(context, unitOfWork, userManager);
            logger.LogInformation("Data seeding completed.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}