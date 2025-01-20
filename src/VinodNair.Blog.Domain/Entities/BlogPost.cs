using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Domain.Entities
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BannerImageUrl { get; set; }
        public string Author { get; set; }
        public BlogStatus Status { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
