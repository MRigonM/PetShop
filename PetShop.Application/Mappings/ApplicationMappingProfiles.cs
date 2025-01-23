using AutoMapper;
using PetShop.Application.Models;
using PetShop.Application.Models.BreedModels;
using PetShop.Application.Models.Pet;
using PetShop.Application.Models.PetImagesModels;
using PetShop.Application.Models.PetModels;
using PetShop.Application.Models.SpeciesModels;
using PetShop.Domain.Entities;

namespace PetShop.Application.Mappings;

public class ApplicationMappingProfiles : Profile
{
    public ApplicationMappingProfiles()
    {
        CreateMap<PetRequest, Pet>().ReverseMap();
        CreateMap<Pet, PetResponse>().ReverseMap();
        CreateMap<PetRequest, PetResponse>().ReverseMap();

        CreateMap<BreedRequest, Breed>().ReverseMap();
        CreateMap<Breed, BreedResponse>().ReverseMap();
        CreateMap<BreedRequest, BreedResponse>().ReverseMap();

        CreateMap<PetImageRequest, PetImage>().ReverseMap();
        CreateMap<PetImage, PetImageResponse>().ReverseMap();
        CreateMap<PetImageRequest, PetImageResponse>().ReverseMap();

        CreateMap<SpeciesRequest, Species>().ReverseMap();
        CreateMap<Species, SpeciesResponse>().ReverseMap();
        CreateMap<SpeciesRequest, SpeciesResponse>().ReverseMap();

        CreateMap<PetCreateRequest, Pet>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UserPetRequest, Pet>().ReverseMap();
        CreateMap<Pet, UserPetResponse>().ReverseMap();
        CreateMap<UserPetRequest, UserPetResponse>().ReverseMap();
    }
}