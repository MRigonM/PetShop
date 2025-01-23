using Microsoft.Extensions.Logging;
using PetShop.Application.Interfaces;
using PetShop.Domain.Common;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;

namespace PetShop.Application.Services;

public class SpeciesService : ISpeciesService
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SpeciesService> _logger;

    public SpeciesService(ISpeciesRepository speciesRepository, IUnitOfWork unitOfWork, ILogger<SpeciesService> logger)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<Result<bool>> CreateSpeciesAsync(Species species)
    {
        try
        {
            _logger.LogInformation("Started creating a Species with Id: {SpeciesId}", species.Id);

            await _speciesRepository.InsertAsync(species);
            var speciesCreated = await _unitOfWork.SaveChangesAsync() > 0;

            if (speciesCreated)
            {
                _logger.LogInformation("Successfully created a Species with Id: {SpeciesId}", species.Id);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to create a Species with Id: {SpeciesId}", species.Id);
            return Result<bool>.Failure(SpeciesErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Species with Id: {SpeciesId}",
                nameof(SpeciesService), species.Id);
            return Result<bool>.Failure(SpeciesErrors.CreationUnexpectedError);
        }
    }

    public async Task<Result<List<Species>>> GetAllSpeciesAsync()
    {
        try
        {
            _logger.LogInformation("Started retrieving all species.");

            var species = await _speciesRepository.GetAllAsync();

            if (species is null)
            {
                return Result<List<Species>>.Failure(SpeciesErrors.RetrievalError);
            }

            _logger.LogInformation("Successfully retrieved all species.");
            return Result<List<Species>>.Success(species.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all species.", nameof(SpeciesService));
            return Result<List<Species>>.Failure(SpeciesErrors.RetrievalError);
        }
    }

    public async Task<Result<Species>> GetSpeciesByIdAsync(int speciesId)
    {
        try
        {
            _logger.LogInformation("Started retrieving Species with Id: {SpeciesId}", speciesId);

            var species = await _speciesRepository.GetByIdAsync(speciesId);

            if (species is null)
            {
                _logger.LogWarning("Species with Id: {SpeciesId} was not found.", speciesId);
                return Result<Species>.Failure(SpeciesErrors.NotFound(speciesId));
            }

            _logger.LogInformation("Successfully retrieved Species with Id: {SpeciesId}", speciesId);
            return Result<Species>.Success(species);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Species with Id: {SpeciesId}",
                nameof(SpeciesService), speciesId);
            return Result<Species>.Failure(SpeciesErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> UpdateSpeciesAsync(Species species)
    {
        try
        {
            _logger.LogInformation("Started updating Species with Id: {SpeciesId}", species.Id);

            await _speciesRepository.UpdateAsync(species);
            var speciesUpdated = await _unitOfWork.SaveChangesAsync() > 0;

            if (speciesUpdated)
            {
                _logger.LogInformation("Successfully updated Species with Id: {SpeciesId}", species.Id);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to update Species with Id: {SpeciesId}. No changes were detected.", species.Id);
            return Result<bool>.Failure(SpeciesErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Species with Id: {SpeciesId}", nameof(SpeciesService), species.Id);
            return Result<bool>.Failure(SpeciesErrors.UpdateUnexpectedError);
        }
    }

    public async Task<Result<bool>> DeleteSpeciesAsync(int speciesId)
    {
        try
        {
            _logger.LogInformation("Started deleting Species with Id: {SpeciesId}", speciesId);

            var species = await _speciesRepository.GetByIdAsync(speciesId);
            if (species is null)
            {
                _logger.LogWarning("Species with Id: {SpeciesId} was not found.", speciesId);
                return Result<bool>.Failure(SpeciesErrors.NotFound(speciesId));
            }

            await _speciesRepository.DeleteAsync(species);
            var speciesDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            if (speciesDeleted)
            {
                _logger.LogInformation("Successfully deleted Species with Id: {SpeciesId}", speciesId);
                return Result<bool>.Success();
            }
            _logger.LogWarning("Failed to delete Species with Id: {SpeciesId}. No changes were detected.", speciesId);
            return Result<bool>.Failure(SpeciesErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Species with Id: {SpeciesId}", nameof(SpeciesService), speciesId);
            return Result<bool>.Failure(SpeciesErrors.DeletionUnexpectedError);
        }
    }
}