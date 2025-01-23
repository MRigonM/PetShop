using Microsoft.Extensions.Logging;
using PetShop.Application.Interfaces;
using PetShop.Domain.Common;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;

namespace PetShop.Application.Services;

public class BreedService : IBreedService
{
    private readonly IBreedRepository _breedRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BreedService> _logger;

    public BreedService(IBreedRepository breedRepository, IUnitOfWork unitOfWork, ILogger<BreedService> logger)
    {
        _breedRepository = breedRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<Result<bool>> CreateBreedAsync(Breed breed)
    {
        try
        {
            _logger.LogInformation("Started creating a Breed with Id: {BreedId}", breed.Id);

            await _breedRepository.InsertAsync(breed);
            var breedCreated = await _unitOfWork.SaveChangesAsync() > 0;

            if (breedCreated)
            {
                _logger.LogInformation("Successfully created a Breed with Id: {BreedId}", breed.Id);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to create a Breed with Id: {BreedId}", breed.Id);
            return Result<bool>.Failure(BreedErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Breed with Id: {BreedId}",
                nameof(BreedService), breed.Id);
            return Result<bool>.Failure(BreedErrors.CreationUnexpectedError);
        }
    }

    public async Task<Result<List<Breed>>> GetAllBreedsAsync()
    {
        try
        {
            _logger.LogInformation("Started retrieving all breeds.");

            var breeds = await _breedRepository.GetAllAsync();

            if (breeds is null)
            {
                return Result<List<Breed>>.Failure(BreedErrors.RetrievalError);
            }

            _logger.LogInformation("Successfully retrieved all breeds.");

            return Result<List<Breed>>.Success(breeds.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all breeds.", nameof(BreedService));
            return Result<List<Breed>>.Failure(BreedErrors.RetrievalError);
        }
    }

    public async Task<Result<Breed>> GetBreedByIdAsync(int breedId)
    {
        try
        {
            _logger.LogInformation("Started retrieving Breed with Id: {BreedId}", breedId);

            var breed = await _breedRepository.GetByIdAsync(breedId);

            if (breed is null)
            {
                _logger.LogWarning("Breed with Id: {BreedId} was not found.", breedId);
                return Result<Breed>.Failure(BreedErrors.NotFound(breedId));
            }

            _logger.LogInformation("Successfully retrieved Breed with Id: {BreedId}", breedId);
            return Result<Breed>.Success(breed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Breed with Id: {BreedId}",
                nameof(BreedService), breedId);
            return Result<Breed>.Failure(BreedErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> UpdateBreedAsync(Breed breed)
    {
        try
        {
            _logger.LogInformation("Started updating Breed with Id: {BreedId}", breed.Id);

            await _breedRepository.UpdateAsync(breed);
            var breedUpdated = await _unitOfWork.SaveChangesAsync() > 0;

            if (breedUpdated)
            {
                _logger.LogInformation("Successfully updated Breed with Id: {BreedId}", breed.Id);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to update Breed with Id: {BreedId}. No changes were detected.", breed.Id);
            return Result<bool>.Failure(BreedErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Breed with Id: {BreedId}",
                nameof(BreedService), breed.Id);
            return Result<bool>.Failure(BreedErrors.UpdateUnexpectedError);
        }
    }

    public async Task<Result<bool>> DeleteBreedAsync(int breedId)
    {
        try
        {
            _logger.LogInformation("Started deleting Breed with Id: {BreedId}", breedId);

            var breed = await _breedRepository.GetByIdAsync(breedId);
            if (breed is null)
            {
                _logger.LogWarning("Breed with Id: {BreedId} was not found.", breedId);
                return Result<bool>.Failure(BreedErrors.NotFound(breedId));
            }

            await _breedRepository.DeleteAsync(breed);
            var breedDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            if (breedDeleted)
            {
                _logger.LogInformation("Successfully deleted Breed with Id: {BreedId}", breedId);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to delete Breed with Id: {BreedId}. No changes were detected.", breedId);
            return Result<bool>.Failure(BreedErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Breed with Id: {BreedId}",
                nameof(BreedService), breedId);
            return Result<bool>.Failure(BreedErrors.DeletionUnexpectedError);
        }
    }
}