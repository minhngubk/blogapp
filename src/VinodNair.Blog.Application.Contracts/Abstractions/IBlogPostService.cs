using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinodNair.Blog.Application.Contracts.Core;
using VinodNair.Blog.Application.Contracts.Dtos;
using VinodNair.Blog.Domain.Entities;

namespace VinodNair.Blog.Application.Contracts.Abstractions
{
    public interface IBlogPostService : IEntityService<BlogPost>
    {
        Task<bool> Approve(Guid Id);
        Task<bool> Reject(Guid Id);
        Task<IEnumerable<BlogPostDto>> GetBlogPostAsync(Guid? editorId);
    }
}
