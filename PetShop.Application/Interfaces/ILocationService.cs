using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ILocationService
{
    Task<Result<Location>> CreateLocationAsync(Location location, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Location>>> GetAllLocationsAsync(CancellationToken cancellationToken);
    Task<Result<Location>> GetLocationByIdAsync(int locationId, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateLocationAsync(Location location, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteLocationAsync(int locationId, CancellationToken cancellationToken);
}