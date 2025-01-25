namespace PetShop.Domain.Interfaces;

public class ISoftDeletion
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    /// <summary>
    /// Restores the entity by undoing the soft deletion.
    /// </summary>
    public void Undo()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}