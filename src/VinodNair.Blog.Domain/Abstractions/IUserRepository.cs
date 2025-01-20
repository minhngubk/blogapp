using Microsoft.AspNetCore.Identity;
using VinodNair.Blog.Domain.Core;
using VinodNair.Blog.Domain.Entities;
namespace VinodNair.Blog.Domain.Abstractions
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAll();
    }
}
