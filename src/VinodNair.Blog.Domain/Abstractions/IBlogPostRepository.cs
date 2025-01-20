using Microsoft.EntityFrameworkCore;
using VinodNair.Blog.Domain.Core;
using VinodNair.Blog.Domain.Entities;

namespace VinodNair.Blog.Domain.Abstractions
{
    public interface IBlogPostRepository : IBaseRepository<BlogPost>
    {
    }
}
