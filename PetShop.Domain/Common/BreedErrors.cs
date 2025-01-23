namespace PetShop.Domain.Common;

public static class BreedErrors
{
    public static Error NotFound(int id) => new Error("Breeds.NotFound", $"Breed with ID {id} was not found.");
    public static Error NoChangesDetected => new Error("Breeds.NoChanges", "No changes were detected during the operation.");
    public static Error CreationFailed => new Error("Breeds.CreationFailed", "Breed creation failed. No changes were made to the database.");
    public static Error CreationUnexpectedError => new Error("Breeds.CreationUnexpectedError", "An unexpected error occurred during breed creation.");
    public static Error RetrievalError => new Error("Breeds.RetrievalError", "An error occurred while retrieving the list of breeds.");
    public static Error UpdateUnexpectedError => new Error("Breeds.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");
    public static Error DeletionUnexpectedError => new Error("Breeds.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}