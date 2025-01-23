namespace PetShop.Domain.Common;

public class FileUploadErrors
{
    public static readonly Error InvalidFile = new Error("FileUpload.InvalidFile", "Please upload a valid file.");
    public static readonly Error UnsupportedFileFormat = new Error("FileUpload.UnsupportedFileFormat ", "File extension type is not allowed.");
    public static readonly Error MaxFileSize = new Error("FileUpload.MaxFileSize", "File size exceeds the maximum allowed size");
    public static readonly Error UnexpectedError = new Error("FileUpload.UnexpectedError", "An unexpected error occurred while uploading the file.");
}