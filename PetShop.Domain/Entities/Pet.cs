namespace PetShop.Domain.Entities;

public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string AgeYears { get; set; }
    public required string About { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<PetImage> PetImages { get; set; } = new List<PetImage>();

}