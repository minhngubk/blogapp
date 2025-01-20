using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Web.Controllers
{
    public class BaseController : Controller
    {
        protected UserRole LoggedInAccountType
        {
            get
            {
                if (User == null)
                    return UserRole.Unknown;

                if (User.IsInRole(UserRole.Admin.ToString()))
                    return UserRole.Admin;
                return UserRole.Editor;
            }
        }

        public string? LoggedInUserName { get { return User.FindFirstValue(ClaimTypes.Name); } }

        public Guid? LoggedInUserId
        {
            get
            {
                var identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Guid.TryParse(identifier, out Guid loggedInUserId);
                return loggedInUserId;
            }
        }
        public BaseController()
        {
        }
    }
}
