using Microsoft.AspNetCore.Http;

namespace VinodNair.Blog.Application.Contracts.Abstractions
{
    public interface IImageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
