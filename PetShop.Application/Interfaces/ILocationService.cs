using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ILocationService
{
    Task<Result<Location>> CreateLocationAsync(Location location);
    Task<Result<IEnumerable<Location>>> GetAllLocationsAsync();
    Task<Result<Location>> GetLocationByIdAsync(int locationId);
    Task<Result<bool>> UpdateLocationAsync(Location location);
    Task<Result<bool>> DeleteLocationAsync(int locationId);
}