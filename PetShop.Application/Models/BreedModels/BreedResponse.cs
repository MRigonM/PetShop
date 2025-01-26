﻿using PetShop.Application.Models.PetModels;
using PetShop.Domain.Entities;

namespace PetShop.Application.Models.BreedModels;

public class BreedResponse
{
    public int Id { get; set; }
    public int SpeciesId { get; set; }
    public Species Species { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<PetResponse> Pets { get; set; } = new List<PetResponse>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}