using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Infrastructure.Data.Seed;

public class SeedUsers
{
    private const string SeedUsersPassword = "Password@123";

    public static async Task SeedUserData(UserManager<IdentityUser> userManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userAsd = new IdentityUser()
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "asd@qwe.com"
        };

        var userJohn = new IdentityUser()
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "john@example.com"
        };

        var userBob = new IdentityUser()
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "bob@example.com"
        };

        var userJane = new IdentityUser()
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "jane@example.com"
        };

        await CreateUserWithClaims(userManager, userAsd, "asdqwe123$A");
        await CreateUserWithClaims(userManager, userJohn, SeedUsersPassword);
        await CreateUserWithClaims(userManager, userBob, SeedUsersPassword);
        await CreateUserWithClaims(userManager, userJane, SeedUsersPassword);
    }

    private static async Task CreateUserWithClaims(UserManager<IdentityUser> userManager, IdentityUser user, string password)
    {
        var createUserResult = await userManager.CreateAsync(user, password);

        if (createUserResult.Succeeded)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimResult = await userManager.AddClaimsAsync(user, userClaims);

            if (!claimResult.Succeeded)
            {
                throw new Exception();
            }
        }
    }
}
