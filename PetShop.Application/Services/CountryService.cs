using Microsoft.Extensions.Logging;
using PetShop.Application.Interfaces;
using PetShop.Domain.Common;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;

namespace PetShop.Application.Services;

public class CountryService: ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CountryService> _logger;

    public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork, ILogger<CountryService> logger)
    {
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> CreateCountryAsync(Country country)
    {
        try
        {
            _logger.LogInformation("Started creating a Country with Id: {CountryId}", country.Id);

            await _countryRepository.InsertAsync(country);
            var countryCreated = await _unitOfWork.SaveChangesAsync() > 0;

            if (countryCreated)
            {
                _logger.LogInformation("Successfully created a Country with Id: {CountryId}", country.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to create a Country with Id: {CountryId}", country.Id);
            return Result<bool>.Failure(CountryErrors.CreationFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Country with Id: {CountryId}",
                             nameof(CountryService), country.Id);
            return Result<bool>.Failure(CountryErrors.CreationUnexpectedError);
        }
    }

    public async Task<Result<List<Country>>> GetAllCountriesAsync()
    {
        try
        {
            _logger.LogInformation("Started retrieving all countries.");

            var countries = await _countryRepository.GetAllAsync();

            if (countries is null)
            {
                _logger.LogError("No countries found");
                return Result<List<Country>>.Failure(CountryErrors.RetrievalError);
            }

            _logger.LogInformation("Successfully retrieved all countries.");
            return Result<List<Country>>.Success(countries.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all countries.", nameof(CountryService));
            return Result<List<Country>>.Failure(CountryErrors.RetrievalError);
        }
    }

    public async Task<Result<Country>> GetCountryByIdAsync(int countryId)
    {
        try
        {
            _logger.LogInformation("Started retrieving Country with Id: {CountryId}", countryId);

            var country = await _countryRepository.GetByIdAsync(countryId);

            if (country is null)
            {
                _logger.LogWarning("Country with Id: {CountryId} was not found.", countryId);
                return Result<Country>.Failure(CountryErrors.NotFound(countryId));
            }

            _logger.LogInformation("Successfully retrieved Country with Id: {CountryId}", countryId);
            return Result<Country>.Success(country);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Country with Id: {CountryId}",
                             nameof(CountryService), countryId);
            return Result<Country>.Failure(CountryErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> UpdateCountryAsync(Country country)
    {
        try
        {
            _logger.LogInformation("Started updating Country with Id: {CountryId}", country.Id);

            await _countryRepository.UpdateAsync(country);
            var countryUpdated = await _unitOfWork.SaveChangesAsync() > 0;

            if (countryUpdated)
            {
                _logger.LogInformation("Successfully updated Country with Id: {CountryId}", country.Id);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update Country with Id: {CountryId}. No changes were detected.", country.Id);
            return Result<bool>.Failure(CountryErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Country with Id: {CountryId}",
                             nameof(CountryService), country.Id);
            return Result<bool>.Failure(CountryErrors.UpdateUnexpectedError);
        }
    }

    public async Task<Result<bool>> DeleteCountryAsync(int countryId)
    {
        try
        {
            _logger.LogInformation("Started deleting Country with Id: {CountryId}", countryId);

            var country = await _countryRepository.GetByIdAsync(countryId);
            if (country is null)
            {
                _logger.LogWarning("Country with Id: {CountryId} was not found.", countryId);
                return Result<bool>.Failure(CountryErrors.NotFound(countryId));
            }

            await _countryRepository.DeleteAsync(country);
            var countryDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            if (countryDeleted)
            {
                _logger.LogInformation("Successfully deleted Country with Id: {CountryId}", countryId);
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to delete Country with Id: {CountryId}. No changes were detected.", countryId);
            return Result<bool>.Failure(CountryErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Country with Id: {CountryId}",
                             nameof(CountryService), countryId);
            return Result<bool>.Failure(CountryErrors.DeletionUnexpectedError);
        }
    }
}
