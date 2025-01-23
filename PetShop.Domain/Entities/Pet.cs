﻿using Microsoft.AspNetCore.Identity;
using PetShop.Domain.Enums;
using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Pet : IEntity<int>
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    /// <summary>
    /// The Id of the breed of the pet
    /// </summary>
    public int BreedId { get; set; }

    /// <summary>
    /// The breed of the pet
    /// </summary>
    public Breed Breed { get; set; }
    
    /// <summary>
    /// The age of the pet in years
    /// </summary>
    public required string AgeYears { get; set; }
    
    /// <summary>
    /// Additional information about the pet
    /// </summary>
    public required string About { get; set; }
    
    /// <summary>
    /// Status of the pet (e.g., 'Pending', 'Approved', 'Rejected')
    /// </summary>
    public PetStatus Status { get; set; }
    
    /// <summary>
    /// The Id of the location where the pet is located
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// The location where the pet is located
    /// </summary>
    public Location Location { get; set; }
    
    /// <summary>
    /// Images of the pet
    /// </summary>
    public ICollection<PetImage> PetImages { get; set; } = new List<PetImage>();
    
    /// <summary>
    /// The ID of the user who posted the pet.
    /// </summary>
    public required string PostedByUserId { get; set; }
    
    /// <summary>
    /// The user who made the request.
    /// </summary>
    public IdentityUser User { get; set; }

}