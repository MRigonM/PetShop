using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class PetImage : IEntity<int>
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
    public required string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime UploadedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}