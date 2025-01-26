using Microsoft.AspNetCore.Identity;
using PetShop.Application.Models.Pet;

namespace PetShop.Application.Models;

public class PaginatedUserPetsResponse
{
    public IEnumerable<UserPetResponse> Pets { get; set; }

    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }
}