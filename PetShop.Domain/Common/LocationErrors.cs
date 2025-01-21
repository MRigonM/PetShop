namespace PetShop.Domain.Common;

public static class LocationErrors
{
    public static Error NotFound(int id) => new Error("Locations.NotFound", $"Location with ID {id} was not found.");
    public static Error NoChangesDetected => new Error("Locations.NoChanges", "No changes were detected during the operation.");
    public static Error CreationFailed => new Error("Locations.CreationFailed", "Location creation failed. No changes were made to the database.");
    public static Error CreationUnexpectedError => new Error("Locations.CreationUnexpectedError", "An unexpected error occurred during location creation.");
    public static Error RetrievalError => new Error("Locations.RetrievalError", "An error occurred while retrieving the list of locations.");
    public static Error UpdateUnexpectedError => new Error("Locations.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");
    public static Error DeletionUnexpectedError => new Error("Locations.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}