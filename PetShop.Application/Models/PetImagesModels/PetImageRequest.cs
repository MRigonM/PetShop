using PetShop.Application.Models.PetModels;

namespace PetShop.Application.Models.PetImagesModels;

public class PetImageRequest
{
    public int Id { get; set; }
    public int PetId { get; set; }
    public PetRequest Pet { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime UploadedAt { get; set; }
}