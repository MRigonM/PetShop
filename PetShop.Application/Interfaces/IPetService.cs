using PetShop.Application.Models;
using PetShop.Application.Models.Pet;
using PetShop.Application.Models.PetModels;
using PetShop.Domain.Common;
using PetShop.Domain.Entities;
using PetShop.Domain.Helpers;

namespace PetShop.Application.Interfaces;

public interface IPetService
{
    Task<Result<int>> CreatePetAsync(PetCreateRequest petCreateRequest);

    /// <summary>
    /// Retrieves a pet by its ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to retrieve.</param>
    /// <returns>A task representing the operation. The task result contains the pet entity.</returns>
    Task<Result<PetResponse>> GetPetByIdAsync(int petId);

    /// <summary>
    /// Updates an existing pet.
    /// </summary>
    /// <param name="pet">The pet entity to update.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> UpdatePetAsync(Pet pet);

    /// <summary>
    /// Deletes a pet by its ID.
    /// </summary>
    /// <param name="petId">The ID of the pet to delete.</param>
    /// <param name="userId">The ID of the user who created the pet.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeletePetAsync(int petId, string userId);

    /// <summary>
    /// Retrieves all pets created by a specific user.
    /// </summary>
    /// <returns>A task representing the operation. The task result contains a collection of pet entities.</returns>
    Task<Result<IEnumerable<UserPetResponse>>> GetPetsByUserIdAsync();

    /// <summary>
    /// Retrieves all pets along with their related entities.
    /// </summary>
    /// <param name="queryParams">The <see cref="QueryParams"/>.</param>
    Task<Result<(List<PetResponse> Pets, int TotalPages)>> GetAvailablePetsWithDetailsAsync(QueryParams queryParams);

    /// <summary>
    /// Retrieves a pet by its unique ID and gets petadoption based on current user
    /// </summary>
    /// <param name="petId">The ID of the pet to retrieve.</param>
    /// <returns>A result containing the pet entity if found, or an error if not found.</returns>
    Task<Result<PetResponse>> GetPetByIdWithUserAdoptionsAsync(int petId);
}