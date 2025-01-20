using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Web.ViewModels
{
    public class AddBlogPostRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public string BannerImageUrl { get; set; }
        public string Author { get; set; }
        public BlogStatus Status { get; set; }
    }
}
