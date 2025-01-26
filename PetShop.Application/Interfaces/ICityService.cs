using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ICityService
{
    Task<Result<bool>> CreateCityAsync(City city, CancellationToken cancellationToken);
    Task<Result<List<City>>> GetAllCitiesAsync(CancellationToken cancellationToken);
    Task<Result<City>> GetCityByIdAsync(int cityId, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateCityAsync(City city, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCityAsync(int cityId, CancellationToken cancellationToken);
}