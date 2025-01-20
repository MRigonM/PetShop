using Microsoft.AspNetCore.Http;
using PetShop.Domain.Common;

namespace PetShop.Application.Interfaces;

public interface IFileUploaderService
{
    Task<Result<string>> UploadFileAsync(IFormFile file);
}