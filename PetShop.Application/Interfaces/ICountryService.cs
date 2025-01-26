using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ICountryService
{
    Task<Result<bool>> CreateCountryAsync(Country country, CancellationToken cancellationToken);
    Task<Result<List<Country>>> GetAllCountriesAsync(CancellationToken cancellationToken);
    Task<Result<Country>> GetCountryByIdAsync(int countryId, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateCountryAsync(Country country, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCountryAsync(int countryId, CancellationToken cancellationToken);
}