using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ICityService
{
    Task<Result<bool>> CreateCityAsync(City city);
    Task<Result<List<City>>> GetAllCitiesAsync();
    Task<Result<City>> GetCityByIdAsync(int cityId);
    Task<Result<bool>> UpdateCityAsync(City city);
    Task<Result<bool>> DeleteCityAsync(int cityId);
}