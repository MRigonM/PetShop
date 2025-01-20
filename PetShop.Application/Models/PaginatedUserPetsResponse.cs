using Microsoft.AspNetCore.Identity;

namespace PetShop.Application.Models;

public class PaginatedUserPetsResponse
{
    public IEnumerable<IdentityUser> Pets { get; set; }

    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }
}