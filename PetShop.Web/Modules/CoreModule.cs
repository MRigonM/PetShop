using Asp.Versioning;
using PetShop.Domain.Abstractions;

namespace PetShop.Modules;

public class CoreModule : IModule
{
    private readonly IConfiguration _configuration;

    public CoreModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Load(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddSignalR();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }
}