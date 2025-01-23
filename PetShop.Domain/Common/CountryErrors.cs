namespace PetShop.Domain.Common;

public class CountryErrors
{
    public static Error NotFound(int id) => new Error("Countries.NotFound", $"Country with ID {id} was not found.");
    public static Error NoChangesDetected => new Error("Countries.NoChanges", "No changes were detected during the operation.");
    public static Error CreationFailed => new Error("Countries.CreationFailed", "Country creation failed. No changes were made to the database.");
    public static Error CreationUnexpectedError => new Error("Countries.CreationUnexpectedError", "An unexpected error occurred during country creation.");
    public static Error RetrievalError => new Error("Countries.RetrievalError", "An error occurred while retrieving the list of countries.");
    public static Error UpdateUnexpectedError => new Error("Countries.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");
    public static Error DeletionUnexpectedError => new Error("Countries.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}