using Asp.Versioning;
using PetShop.Application.Mappings;
using PetShop.Domain.Abstractions;
using PetShop.Mappings;

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
        services.Configure<ApiSettings>(_configuration.GetSection(ApiSettings.SectionName));
        services.AddControllersWithViews();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddAutoMapper(typeof(ApplicationMappingProfiles).Assembly, typeof(WebMappingProfile).Assembly);
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