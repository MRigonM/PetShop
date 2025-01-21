namespace PetShop.Domain.Common;

public static class PetErrors
{
    public static Error NotFound(int id) => new Error("Pets.NotFound", $"Pet with ID {id} was not found.");
    public static Error NoPetsFound() => new Error("Pets.NoPetsFound", "No pets were found.");
    public static Error NoChangesDetected => new Error("Pets.NoChanges", "No changes were detected during the operation.");
    public static Error CreationFailed => new Error("Pets.CreationFailed", "Pet creation failed. No changes were made to the database.");
    public static Error CreationUnexpectedError => new Error("Pets.CreationUnexpectedError", "An unexpected error occurred during pet creation.");
    public static Error RetrievalError => new Error("Pets.RetrievalError", "An error occurred while retrieving the list of pets.");
    public static Error UpdateUnexpectedError => new Error("Pets.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");
    public static Error DeletionUnexpectedError => new Error("Pets.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
    public static Error Unauthorized => new Error("Pets.Unauthorized", "You are not authorized to perform this operation.");
    public static Error NoPetsFoundForUser(string userId) => new Error("Pets.NoPetsFoundForUser", $"No pets were found for user with ID {userId}.");
}