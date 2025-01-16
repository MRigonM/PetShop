namespace PetShop.Domain.Entities;

public class PetImage
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
    public required string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
}