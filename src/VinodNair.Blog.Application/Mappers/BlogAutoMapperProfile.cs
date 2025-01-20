using AutoMapper;
using VinodNair.Blog.Application.Contracts.Dtos;
using VinodNair.Blog.Domain.Entities;

namespace VinodNair.Blog.Application.Mappers
{
    public class BlogAutoMapperProfile : Profile
    {
        public BlogAutoMapperProfile()
        {
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<BlogPostDto, BlogPost>();
        }

    }
}
