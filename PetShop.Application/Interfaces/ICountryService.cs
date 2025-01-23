using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ICountryService
{
    Task<Result<bool>> CreateCountryAsync(Country country);
    Task<Result<List<Country>>> GetAllCountriesAsync();
    Task<Result<Country>> GetCountryByIdAsync(int countryId);
    Task<Result<bool>> UpdateCountryAsync(Country country);
    Task<Result<bool>> DeleteCountryAsync(int countryId);
}