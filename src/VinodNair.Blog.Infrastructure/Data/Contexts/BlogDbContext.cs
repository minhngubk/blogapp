using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VinodNair.Blog.Domain.Entities;

namespace VinodNair.Blog.Infrastructure.Data.Contexts
{
    public class BlogDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserClaim<string>
                                                    , ApplicationUserRole, IdentityUserLogin<string>
                                                    , IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            CustomIdentityTables(builder);

            InitSeedData(builder);

        }

        protected void CustomIdentityTables(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "AspNetUsers");
            });

            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable(name: "AspNetUserRoles");
            });

            builder.Entity<ApplicationUser>()
               .HasMany(x => x.UserRoles)
               .WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            builder.Entity<ApplicationUserRole>()
              .HasOne(x => x.Role)
              .WithMany()
              .HasForeignKey(x => x.RoleId);
        }
        protected void InitSeedData(ModelBuilder builder)
        {
            var adminRoleId = "4dcbe6fe-4398-49de-8410-5548d273620e";
            var editorRoleId = "caab0a91-d60a-41dc-b262-b91c10dc6d62";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "Editor",
                    NormalizedName = "Editor",
                    Id = editorRoleId,
                    ConcurrencyStamp = editorRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed AdminUser
            var adminId = "84d97bfd-8f03-457d-9c5d-63c2d3fc40e3";
            var adminUser = new ApplicationUser
            {
                UserName = "vinodnair@Blog.com",
                Email = "vinodnair@Blog.com",
                NormalizedEmail = "vinodnair@Blog.com".ToUpper(),
                NormalizedUserName = "vinodnair@Blog.com".ToUpper(),
                Id = adminId,
                FullName = "Vinod Nair"
            };

            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(adminUser, "Admin@123");


            builder.Entity<ApplicationUser>().HasData(adminUser);


            // Add all the roles to AdminUser
            var adminRoles = new List<ApplicationUserRole>
            {
                new ApplicationUserRole
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                },
                new ApplicationUserRole
                {
                    RoleId = editorRoleId,
                    UserId = adminId
                }
            };

            builder.Entity<ApplicationUserRole>().HasData(adminRoles);


            // Seed Editor user
            var editorId = "be8d7382-43a3-42d7-8ba7-befce2879fa5";
            var editorUser = new ApplicationUser
            {
                UserName = "editor@Blog.com",
                Email = "editor@Blog.com",
                NormalizedEmail = "editor@Blog.com".ToUpper(),
                NormalizedUserName = "editor@Blog.com".ToUpper(),
                Id = editorId,
                FullName = "editor"
            };

            editorUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(editorUser, "Editor@123");


            builder.Entity<ApplicationUser>().HasData(editorUser);


            // Add all the roles to AdminUser
            var editorRoles = new List<ApplicationUserRole>
            {
                new ApplicationUserRole
                {
                    RoleId = editorRoleId,
                    UserId = editorId
                }
            };

            builder.Entity<ApplicationUserRole>().HasData(editorRoles);
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
