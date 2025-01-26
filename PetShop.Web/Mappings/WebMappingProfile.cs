using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetShop.Application.Models;
using PetShop.Application.Models.BreedModels;
using PetShop.Application.Models.Pet;
using PetShop.Application.Models.PetImagesModels;
using PetShop.Application.Models.PetModels;
using PetShop.Application.Models.SpeciesModels;
using PetShop.Domain.Entities;
using PetShop.Models;
using PetShop.Models.Breed;
using PetShop.Models.Location;
using PetShop.Models.Pet;
using PetShop.Models.PetImage;
using PetShop.Models.Species;

namespace PetShop.Mappings;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<UserPetResponse, UserPetViewModel>().ReverseMap();
        CreateMap<UserPetRequest, UserPetViewModel>().ReverseMap();

        CreateMap<PetResponse, PetViewModel>().ReverseMap();
        CreateMap<PetResponse, PetViewModel>();
        CreateMap<PetRequest, PetViewModel>().ReverseMap();

        CreateMap<PetCreateViewModel, PetCreateRequest>().ReverseMap();

        CreateMap<Pet, PetViewModel>().ReverseMap();
        ; 

        CreateMap<BreedResponse, BreedViewModel>().ReverseMap();
        CreateMap<BreedRequest, BreedViewModel>().ReverseMap();

        CreateMap<Breed, BreedViewModel>()
            .ReverseMap(); 

        CreateMap<PetImageResponse, PetImageViewModel>().ReverseMap();
        CreateMap<PetImageRequest, PetImageViewModel>().ReverseMap();

        CreateMap<PetImage, PetImageViewModel>().ReverseMap();

        CreateMap<SpeciesResponse, SpeciesViewModel>().ReverseMap();
        CreateMap<SpeciesRequest, SpeciesViewModel>().ReverseMap();

        CreateMap<Species, SpeciesViewModel>()
            .ReverseMap(); 

        CreateMap<ApplicationUser, UserViewModel>()
            .ReverseMap(); 

        CreateMap<Location, LocationViewModel>()
            .ReverseMap(); 

        CreateMap<City, CityViewModel>()
            .ReverseMap(); 

        CreateMap<Country, CountryViewModel>()
            .ReverseMap(); 

        CreateMap<PetCreateViewModel, PetCreateRequest>();
    }
}