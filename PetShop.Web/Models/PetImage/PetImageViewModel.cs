namespace PetShop.Models.PetImage;

public class PetImageViewModel
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public bool IsPrimary { get; set; }
}