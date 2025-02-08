using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;

public class SeedUsers
{
    private const string SeedUsersPassword = "Password@123";

    public static async Task SeedUserData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure roles exist
        await EnsureRoleExists(roleManager, "User");
        await EnsureRoleExists(roleManager, "Admin");

        if (await userManager.Users.AnyAsync()) return;

        var userAsd = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "asd@qwe.com",
            FirstName = "Tester",
            LastName = "Maverick",
        };

	var userJohn = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "john@example.com",
            FirstName = "john",
            LastName = "doe"
        };

        var userBob = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "bob@example.com",
            FirstName = "bob",
            LastName = "doe",
            IsDeleted = false,
            Address = "123 Main St"
        };

        var userJane = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "jane@example.com",
            FirstName = "jane",
            LastName = "doe"
        };

        var adminUser = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "User"
        };
	
	
        await CreateUserWithRole(userManager, userAsd, "User", SeedUsersPassword);
	    await CreateUserWithRole(userManager, userJohn, "User", SeedUsersPassword);
        await CreateUserWithRole(userManager, userBob, "User", SeedUsersPassword);
        await CreateUserWithRole(userManager, userJane, "User", SeedUsersPassword);
        await CreateUserWithRole(userManager, adminUser, "Admin", SeedUsersPassword);
    }

    private static async Task EnsureRoleExists(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task CreateUserWithRole(UserManager<ApplicationUser> userManager, ApplicationUser user, string role, string password)
    {
        var createUserResult = await userManager.CreateAsync(user, password);

        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, role)
            };

            var claimResult = await userManager.AddClaimsAsync(user, userClaims);

            if (!claimResult.Succeeded)
            {
                throw new Exception("Failed to assign claims.");
            }
        }
    }
}