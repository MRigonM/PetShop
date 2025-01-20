namespace PetShop.Domain.Common;

public class Error(string Code, string Description)
{
    public string Code { get; } = Code;
    public string Description { get; } = Description;
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("General.Null", "Null value was provided");
    
}