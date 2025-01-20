using Microsoft.EntityFrameworkCore;
using VinodNair.Blog.Domain.Abstractions;
using VinodNair.Blog.Domain.Entities;
using VinodNair.Blog.Infrastructure.Data.Contexts;
using VinodNair.Blog.Infrastructure.Data.Core;

namespace VinodNair.Blog.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly BlogDbContext blogDbContext;

        public UserRepository(BlogDbContext authDbContext) : base(authDbContext)
        {
            this.blogDbContext = authDbContext;
        }


        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var users = await blogDbContext
                .Users.Include(x => x.UserRoles)
                .ThenInclude(x => x.Role).ToListAsync();
            return users;
        }
    }
}
