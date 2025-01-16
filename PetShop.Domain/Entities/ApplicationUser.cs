﻿namespace PetShop.Domain.Entities;

public class ApplicationUser
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The user's address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The user's address.
    /// </summary>
    public string? ImageUrl { get; set; }
    
    /// <summary>
    /// A collection of pets owned by the user.
    /// </summary>
    public ICollection<Pet> Pets { get; set; }

    /// <summary>
    /// A collection of adoption requests made by the user.
    /// </summary>
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; }

}