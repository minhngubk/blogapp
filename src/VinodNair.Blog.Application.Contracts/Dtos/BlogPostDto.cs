using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Application.Contracts.Dtos
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BannerImageUrl { get; set; }
        public string Author { get; set; }
        public BlogStatus Status { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
