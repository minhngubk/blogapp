using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinodNair.Blog.Application.Contracts.Abstractions;
using VinodNair.Blog.Application.Contracts.Dtos;
using VinodNair.Blog.Application.Services.Core;
using VinodNair.Blog.Domain.Abstractions;
using VinodNair.Blog.Domain.Core;
using VinodNair.Blog.Domain.Entities;
using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Application.Services
{
    public class BlogPostService : EntityService<BlogPost>, IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper mapper;

        public BlogPostService(IUnitOfWork unitOfWork, IBlogPostRepository blogPostRepository, IMapper mapper) : base(unitOfWork, blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Approve(Guid Id)
        {
            var existBlog = await _blogPostRepository.FindAsync(x => x.Id == Id);
            if (existBlog != null && existBlog.Status == BlogStatus.Draft)
            {
                existBlog.Status = BlogStatus.Published;
                existBlog.PublishedDate = DateTime.UtcNow;
                await _blogPostRepository.UpdateAsync(existBlog);
                UnitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> Reject(Guid Id)
        {
            var existBlog = await _blogPostRepository.FindAsync(x => x.Id == Id);
            if (existBlog != null && existBlog.Status == BlogStatus.Draft)
            {
                existBlog.Status = BlogStatus.Rejected;
                existBlog.RejectedDate = DateTime.UtcNow;
                await _blogPostRepository.UpdateAsync(existBlog);
                UnitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<BlogPostDto>> GetBlogPostAsync(Guid? editorId)
        {

            var queryable = _blogPostRepository.GetQueryable();

            if (editorId != null)
            {
                queryable = queryable.Where(x => x.CreatedBy == editorId);
            }

            //TODO : implement pagination
            var blogPosts = await queryable.Select(x => mapper.Map<BlogPostDto>(x)).ToListAsync();
            return blogPosts;

        }
    }
}
