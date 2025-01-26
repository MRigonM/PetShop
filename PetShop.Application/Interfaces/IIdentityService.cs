using PetShop.Application.Models.Identity;
using PetShop.Domain.Common;

namespace PetShop.Application.Interfaces;

public interface IIdentityService
{
    Task<Result<bool>> RegisterAsync(RegisterRequest request);
    Task<Result<bool>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}