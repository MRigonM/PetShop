using Microsoft.AspNetCore.Http;
using PetShop.Domain.Enums;

namespace PetShop.Application.Models;

public class PetCreateRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BreedId { get; set; }
    public string AgeYears { get; set; }
    public string About { get; set; }
    public int LocationId { get; set; }
    public int CityId { get; set; }
    public string PostedByUserId { get; set; }
    public string Address { get; set; }
    public PetStatus Status { get; set; }
    public IEnumerable<IFormFile> ImageFiles { get; set; }
}