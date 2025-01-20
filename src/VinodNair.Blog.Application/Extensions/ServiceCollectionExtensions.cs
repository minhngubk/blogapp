using Microsoft.Extensions.DependencyInjection;
using VinodNair.Blog.Application.Contracts.Abstractions;
using VinodNair.Blog.Application.Mappers;
using VinodNair.Blog.Application.Services;

namespace VinodNair.Blog.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogApplication(this IServiceCollection services)
        {
            //Register Services
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<IImageService, CloudinaryImageService>();
            services.AddAutoMapper(typeof(BlogAutoMapperProfile));

            return services;
        }
    }
}
