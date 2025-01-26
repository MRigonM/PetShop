using System.Configuration;
using Microsoft.EntityFrameworkCore;
using PetShop.Application.Interfaces;
using PetShop.Application.Services;
using PetShop.Domain.Abstractions;
using PetShop.Domain.Helpers;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Data;
using PetShop.Infrastructure.Repositories;
using PetShop.Infrastructure.Services;

namespace PetShop.Modules;

public class DataModule : IModule
{
    private readonly IConfiguration _configuration;

    public DataModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Load(IServiceCollection services)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (connectionString is null)
        {
            throw new ConfigurationErrorsException("Cannot find 'DefaultConnection' section inside the configuration");
        }

        services.AddDbContext<AppDbContext>(options =>
            options
                .UseSqlServer(connectionString)
                .AddInterceptors(new SoftDeleteInterceptor())
        );
        
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<QueryParams>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IBreedRepository, BreedRepository>();
        services.AddScoped<IFileUploaderService, FileUploaderService>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddScoped<IPetImageRepository, PetImageRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<ISpeciesService, SpeciesService>();
        services.AddScoped<IBreedService, BreedService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICityService, CityService>();

    }
}