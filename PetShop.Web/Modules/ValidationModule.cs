using FluentValidation;
using PetShop.Domain.Abstractions;
using PetShop.Models.Identity;
using PetShop.Models.Pet;
using PetShop.Validators;

namespace PetShop.Modules;

public class ValidationModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterViewModel>();
        services.AddScoped<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
        services.AddScoped<IValidator<LoginViewModel>,LoginModelValidator>();
        services.AddScoped<IValidator<LoginViewModel>, LoginModelValidator>();
        services.AddScoped<IValidator<PetCreateViewModel>, PetCreateViewModelValidator>();
    }
}