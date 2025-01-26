using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PetShop.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PetShop.Web"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.EnableSensitiveDataLogging(true);
        builder.UseSqlServer(connectionString);

        return new AppDbContext(builder.Options, configuration);
    }
}