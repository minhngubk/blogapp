using Microsoft.AspNetCore.Mvc;
using System.Net;
using VinodNair.Blog.Application.Contracts.Abstractions;

namespace VinodNair.Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            this._imageService = imageService;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call a repository
            var imageURL = await _imageService.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageURL });
        }
    }
}
