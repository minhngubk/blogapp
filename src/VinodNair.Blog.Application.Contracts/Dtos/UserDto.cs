using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Application.Contracts.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
    }
}
