using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinodNair.Blog.Domain.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        //public byte[]? Picture { get; set; }
    }
}
