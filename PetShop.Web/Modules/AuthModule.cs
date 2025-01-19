using Microsoft.AspNetCore.Identity;
using PetShop.Application.Interfaces;
using PetShop.Application.Services;
using PetShop.Domain.Abstractions;
using PetShop.Infrastructure.Data;

namespace PetShop.Modules;

public class AuthModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services
            .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserAccessor, UserAccessor>();
    }
}