using Microsoft.AspNetCore.Identity;

namespace PetShop.Domain.Entities;

public class ApplicationUser : IdentityUser
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
    /// The date and time when the user was created.
    /// Default is the current date and time.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// A collection of pets owned by the user.
    /// </summary>
    public ICollection<Pet> Pets { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is soft-deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the entity was soft-deleted.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}