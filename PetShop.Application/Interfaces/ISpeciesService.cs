using PetShop.Domain.Common;
using PetShop.Domain.Entities;

namespace PetShop.Application.Interfaces;

public interface ISpeciesService
{
    Task<Result<bool>> CreateSpeciesAsync(Species species);
    Task<Result<List<Species>>> GetAllSpeciesAsync();
    Task<Result<Species>> GetSpeciesByIdAsync(int speciesId);
    Task<Result<bool>> UpdateSpeciesAsync(Species species);
    Task<Result<bool>> DeleteSpeciesAsync(int speciesId);
}