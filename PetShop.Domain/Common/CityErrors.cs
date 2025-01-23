namespace PetShop.Domain.Common;

public static class CityErrors
{
    public static Error NotFound(int id) => new Error("Cities.NotFound", $"City with ID {id} was not found.");
    public static Error NoChangesDetected => new Error("Cities.NoChanges", "No changes were detected during the operation.");
    public static Error CreationFailed => new Error("Cities.CreationFailed", "City creation failed. No changes were made to the database.");
    public static Error CreationUnexpectedError => new Error("Cities.CreationUnexpectedError", "An unexpected error occurred during city creation.");
    public static Error RetrievalError => new Error("Cities.RetrievalError", "An error occurred while retrieving the list of cities.");
    public static Error UpdateUnexpectedError => new Error("Cities.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");
    public static Error DeletionUnexpectedError => new Error("Cities.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");

}