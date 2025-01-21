using Microsoft.Extensions.Logging;
using PetShop.Application.Interfaces;
using PetShop.Domain.Common;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;

namespace PetShop.Application.Services;

public class LocationService: ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LocationService> _logger;

    public LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork, ILogger<LocationService> logger)
    {
        _locationRepository = locationRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<Result<Location>> CreateLocationAsync(Location location)
    {
        try
        {
            _logger.LogInformation("Started creating a Location with Id: {LocationId}", location.Id);

            await _locationRepository.InsertAsync(location);
            var locationCreated = await _unitOfWork.SaveChangesAsync() > 0;

            if (locationCreated)
            {
                _logger.LogInformation("Successfully created a Location with Id: {LocationId}", location.Id);
                return Result<Location>.Success(location);
            }

            _logger.LogWarning("Failed to create a Location with Id: {LocationId}", location.Id);
            return Result<Location>.Failure(LocationErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Location with Id: {LocationId}",
                             nameof(LocationService), location.Id);
            return Result<Location>.Failure(LocationErrors.CreationUnexpectedError);
        }
    }

    public async Task<Result<IEnumerable<Location>>> GetAllLocationsAsync()
    {
        try
        {
            _logger.LogInformation("Started retrieving all locations.");

            var locations = await _locationRepository.GetAllAsync();

            _logger.LogInformation("Successfully retrieved all locations.");
            return Result<IEnumerable<Location>>.Success(locations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all locations.",
                             nameof(LocationService));
            return Result<IEnumerable<Location>>.Failure(LocationErrors.RetrievalError);
        }
    }

    public async Task<Result<Location>> GetLocationByIdAsync(int locationId)
    {
        try
        {
            _logger.LogInformation("Started retrieving Location with Id: {LocationId}", locationId);

            var location = await _locationRepository.GetByIdAsync(locationId);

            if (location is null)
            {
                _logger.LogWarning("Location with Id: {LocationId} was not found.", locationId);
                return Result<Location>.Failure(LocationErrors.NotFound(locationId));
            }

            _logger.LogInformation("Successfully retrieved Location with Id: {LocationId}", locationId);
            return Result<Location>.Success(location);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Location with Id: {LocationId}",
                             nameof(LocationService), locationId);
            return Result<Location>.Failure(LocationErrors.RetrievalError);
        }
    }
    
    public async Task<Result<bool>> UpdateLocationAsync(Location location)
    {
        try
        {
            _logger.LogInformation("Started updating Location with Id: {LocationId}", location.Id);

            await _locationRepository.UpdateAsync(location);
            var locationUpdated = await _unitOfWork.SaveChangesAsync() > 0;

            if (locationUpdated)
            {
                _logger.LogInformation("Successfully updated Location with Id: {LocationId}", location.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update Location with Id: {LocationId}. No changes were detected.", location.Id);
            return Result<bool>.Failure(LocationErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Location with Id: {LocationId}",
                             nameof(LocationService), location.Id);
            return Result<bool>.Failure(LocationErrors.UpdateUnexpectedError);
        }
    }
    
    public async Task<Result<bool>> DeleteLocationAsync(int locationId)
    {
        try
        {
            _logger.LogInformation("Started deleting Location with Id: {LocationId}", locationId);

            var location = await _locationRepository.GetByIdAsync(locationId);
            if (location is null)
            {
                _logger.LogWarning("Location with Id: {LocationId} was not found.", locationId);
                return Result<bool>.Failure(LocationErrors.NotFound(locationId));
            }

            await _locationRepository.DeleteAsync(location);
            var locationDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            if (locationDeleted)
            {
                _logger.LogInformation("Successfully deleted Location with Id: {LocationId}", locationId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to delete Location with Id: {LocationId}. No changes were detected.", locationId);
            return Result<bool>.Failure(LocationErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Location with Id: {LocationId}",
                             nameof(LocationService), locationId);
            return Result<bool>.Failure(LocationErrors.DeletionUnexpectedError);
        }
    }
}