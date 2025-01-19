using Microsoft.Extensions.DependencyInjection;

namespace PetShop.Domain.Abstractions;

public interface IModule
{
    /// <summary>
    /// Loads the Dependency Injection Registrations.
    /// </summary>
    void Load(IServiceCollection services);
}