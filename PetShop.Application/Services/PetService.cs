using AutoMapper;
using Microsoft.Extensions.Logging;
using PetShop.Application.Interfaces;
using PetShop.Application.Models;
using PetShop.Application.Models.Pet;
using PetShop.Application.Models.PetModels;
using PetShop.Domain.Common;
using PetShop.Domain.Entities;
using PetShop.Domain.Enums;
using PetShop.Domain.Helpers;
using PetShop.Domain.Interfaces;

namespace PetShop.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PetService> _logger;
    private readonly IFileUploaderService _fileUploaderService;
    private readonly IPetImageRepository _petImageRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;
    private readonly ILocationService _locationService;

    public PetService(IPetRepository petRepository,
        IUnitOfWork unitOfWork,
        ILogger<PetService> logger,
        IFileUploaderService fileUploaderService,
        IPetImageRepository petImageRepository,
        IUserAccessor userAccessor,
        IMapper mapper,
        ILocationService locationService)
    {
        _petRepository = petRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _fileUploaderService = fileUploaderService;
        _petImageRepository = petImageRepository;
        _userAccessor = userAccessor;
        _mapper = mapper;
        _locationService = locationService;
    }

    public async Task<Result<(List<PetResponse> Pets, int TotalPages)>> GetAvailablePetsWithDetailsAsync(
        QueryParams queryParams)
    {
        try
        {
            var pets = await _petRepository.GetAvailablePetsWithDetailsAsync(queryParams);
            var totalPages = (int)Math.Ceiling(pets.TotalCount / (decimal)queryParams.PageSize);

            var petResponses = _mapper.Map<List<PetResponse>>(pets.Pets);

            return Result<(List<PetResponse> Pets, int TotalPages)>.Success((petResponses, totalPages));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to retrieve all pets with related entities",
                nameof(PetService));
            return Result<(List<PetResponse> Pets, int TotalPages)>.Failure(PetErrors.RetrievalError);
        }
    }

    public async Task<Result<int>> CreatePetAsync(PetCreateRequest petCreateRequest)
    {
        try
        {
            petCreateRequest.PostedByUserId = _userAccessor.GetUserId();

            var location = new Location
            {
                CityId = petCreateRequest.CityId,
                Address = petCreateRequest.Address,
            };

            var locationInsertResult = await _locationService.CreateLocationAsync(location);

            if (!locationInsertResult.IsSuccess)
            {
                _logger.LogError("Failed to create a location for the pet with Id: {PetId}", petCreateRequest.Id);
                return Result<int>.Failure(LocationErrors.CreationFailed);
            }

            var pet = _mapper.Map<Pet>(petCreateRequest);
            pet.Location = locationInsertResult.Value;
            pet.PostedByUserId = _userAccessor.GetUserId();
            pet.Status = PetStatus.Available;

            var petId = await _petRepository.InsertAsync(pet);
            var petCreated = await _unitOfWork.SaveChangesAsync() > 0;

            if (petCreated is false)
            {
                _logger.LogError("Failed to create a Pet with Id: {PetId} from UserId: {UserId}", pet.Id,
                    pet.PostedByUserId);
                return Result<int>.Failure(PetErrors.CreationFailed);
            }

            var imageFilesList = petCreateRequest.ImageFiles.ToList();

            for (var i = 0; i < imageFilesList.Count; i++)
            {
                var imageFile = imageFilesList[i];

                var uploadedFileName = await _fileUploaderService.UploadFileAsync(imageFile);
                var fileName = uploadedFileName.Value;

                var petImage = new PetImage
                {
                    PetId = pet.Id,
                    ImageUrl = fileName,
                    IsPrimary = i is 0,
                    Pet = pet
                };

                await _petImageRepository.InsertAsync(petImage);
            }

            var arePetImagesSaved = await _unitOfWork.SaveChangesAsync() > 0;

            if (arePetImagesSaved is false)
            {
                _logger.LogError("Failed to save pet images; the pet creation process cannot be completed.");
                return Result<int>.Failure(PetErrors.CreationFailed);
            }

            return Result<int>.Success(pet.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in {ServiceName} while attempting to create a Pet for UserId: {UserId}",
                nameof(PetService),
                petCreateRequest.PostedByUserId);
            return Result<int>.Failure(PetErrors.CreationUnexpectedError);
        }
    }

    public async Task<Result<PetResponse>> GetPetByIdAsync(int petId)
    {
        try
        {
            var pet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(petId);

            if (pet is null)
            {
                _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                return Result<PetResponse>.Failure(PetErrors.NotFound(petId));
            }

            var petResponse = _mapper.Map<PetResponse>(pet);

            return Result<PetResponse>.Success(petResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to retrieve Pet with Id: {PetId}",
                nameof(PetService), petId);
            return Result<PetResponse>.Failure(PetErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> DeletePetAsync(int petId, string userId)
    {
        try
        {
            var pet = await _petRepository.GetByIdAsync(petId);
            if (pet is null)
            {
                _logger.LogWarning("Pet with Id: {PetId} was not found.", petId);
                return Result<bool>.Failure(PetErrors.NotFound(petId));
            }

            if (pet.PostedByUserId != userId)
            {
                _logger.LogWarning("User {UserId} is not authorized to delete Pet with Id: {PetId}", userId, petId);
                return Result<bool>.Failure(PetErrors.Unauthorized);
            }

            await _petRepository.UpdateAsync(pet);

            _logger.LogWarning("Failed to soft-delete Pet with Id: {PetId}. No changes were detected.", petId);
            return Result<bool>.Failure(PetErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to delete Pet with Id: {PetId}",
                nameof(PetService), petId);
            return Result<bool>.Failure(PetErrors.DeletionUnexpectedError);
        }
    }

    public async Task<Result<IEnumerable<UserPetResponse>>> GetPetsByUserIdAsync()
    {
        try
        {
            var userId = _userAccessor.GetUserId();

            var pets = await _petRepository.GetByUserIdAsync(userId);

            if (!pets.Any())
            {
                return Result<IEnumerable<UserPetResponse>>.Failure(PetErrors.NoPetsFoundForUser(userId));
            }

            var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);
            return Result<IEnumerable<UserPetResponse>>.Success(petResponses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to retrieve pets for UserId: {UserId}",
                nameof(PetService), _userAccessor.GetUserId());
            return Result<IEnumerable<UserPetResponse>>.Failure(PetErrors.RetrievalError);
        }
    }

    public async Task<Result<bool>> UpdatePetAsync(Pet pet)
    {
        try
        {
            var userId = _userAccessor.GetUserId();

            var existingPet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(pet.Id);
            if (existingPet is null)
            {
                _logger.LogWarning("Pet with Id: {PetId} not found", pet.Id);
                return Result<bool>.Failure(PetErrors.NoPetsFound());
            }

            if (existingPet.PostedByUserId != userId)
            {
                _logger.LogWarning("UserId: {UserId} is not authorized to update Pet with Id: {PetId}", userId, pet.Id);
                return Result<bool>.Failure(UsersErrors.Unauthorized);
            }

            existingPet.Name = pet.Name;
            existingPet.AgeYears = pet.AgeYears;
            existingPet.About = pet.About;
            existingPet.BreedId = pet.BreedId;

            var petUpdated = await _unitOfWork.SaveChangesAsync() > 0;

            if (petUpdated)
            {
                return Result<bool>.Success();
            }

            _logger.LogWarning("Failed to update Pet with Id: {PetId} from UserId: {UserId}. No changes were detected.",
                pet.Id, userId);
            return Result<bool>.Failure(PetErrors.NoChangesDetected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to update Pet with Id: {PetId} for UserId: {UserId}",
                nameof(PetService), pet.Id, _userAccessor.GetUserId());
            return Result<bool>.Failure(PetErrors.UpdateUnexpectedError);
        }
    }

    public async Task<Result<PetResponse>> GetPetByIdWithUserAdoptionsAsync(int petId)
    {
        try
        {
            var userId = _userAccessor.GetUserId();
            var pet = await _petRepository.GetPetByIdWithRelatedEntitiesAsync(petId, userId);

            if (pet is null)
            {
                _logger.LogError("Pet with Id: {PetId} was not found.", petId);
                return Result<PetResponse>.Failure(PetErrors.NotFound(petId));
            }

            var petResponse = _mapper.Map<PetResponse>(pet);

            return Result<PetResponse>.Success(petResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An error occurred in the {ServiceName} while attempting to retrieve Pet with Id: {PetId}",
                nameof(PetService), petId);
            return Result<PetResponse>.Failure(PetErrors.RetrievalError);
        }
    }
}