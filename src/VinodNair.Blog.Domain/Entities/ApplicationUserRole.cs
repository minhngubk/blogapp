using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinodNair.Blog.Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual IdentityRole Role { get; set; }
    }
}
