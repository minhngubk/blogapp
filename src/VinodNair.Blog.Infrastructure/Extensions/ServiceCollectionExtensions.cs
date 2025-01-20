using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinodNair.Blog.Application.Contracts.Abstractions;
using VinodNair.Blog.Application.Services;
using VinodNair.Blog.Domain.Abstractions;
using VinodNair.Blog.Domain.Core;
using VinodNair.Blog.Domain.Entities;
using VinodNair.Blog.Infrastructure.Data.Contexts;
using VinodNair.Blog.Infrastructure.Data.Core;
using VinodNair.Blog.Infrastructure.Data.Repositories;

namespace VinodNair.Blog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<BlogDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("BlogDbConnectionString")), ServiceLifetime.Scoped);
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<BlogDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
