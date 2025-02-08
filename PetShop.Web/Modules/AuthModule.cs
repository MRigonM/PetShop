using Microsoft.AspNetCore.Identity;
using PetShop.Application.Interfaces;
using PetShop.Application.Services;
using PetShop.Domain.Abstractions;
using PetShop.Domain.Entities;
using PetShop.Infrastructure.Data;

namespace PetShop.Modules;

public class AuthModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Users/Login";
        });
    }
}