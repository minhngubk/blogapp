using VinodNair.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using VinodNair.Blog.Infrastructure.Data.Contexts;
using VinodNair.Blog.Infrastructure.Data.Core;
using VinodNair.Blog.Domain.Abstractions;

namespace VinodNair.Blog.Infrastructure.Data.Repositories
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {
        private readonly BlogDbContext blogDbContext;

        public BlogPostRepository(BlogDbContext bloggieDbContext) : base(bloggieDbContext)
        {
            this.blogDbContext = bloggieDbContext;
        }
    }
}
