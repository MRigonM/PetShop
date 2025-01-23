namespace PetShop.Domain.Common;

public class SpeciesErrors
{
    public static Error NotFound(int id) => new Error("Species.NotFound", $"Species with ID {id} was not found.");
    public static Error NoChangesDetected => new Error("Species.NoChanges", "No changes were detected during the operation.");
    public static Error CreationFailed => new Error("Species.CreationFailed", "Species creation failed. No changes were made to the database.");
    public static Error CreationUnexpectedError => new Error("Species.CreationUnexpectedError", "An unexpected error occurred during species creation.");
    public static Error RetrievalError => new Error("Species.RetrievalError", "An error occurred while retrieving the list of species.");
    public static Error UpdateUnexpectedError => new Error("Species.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");
    public static Error DeletionUnexpectedError => new Error("Species.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");

}