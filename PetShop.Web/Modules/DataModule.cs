using System.Configuration;
using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Abstractions;
using PetShop.Infrastructure.Data;

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
            options.UseSqlServer(connectionString)
        );
        services.AddDatabaseDeveloperPageExceptionFilter();
    }
}