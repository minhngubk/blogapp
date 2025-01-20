using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Web.ViewModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }

        public string Roles { get; set; }

        public string AccountType
        {
            get
            {
                if (string.IsNullOrEmpty(Roles))
                    return "Unknown";
                if (Roles.Contains(UserRole.Admin.ToString()))
                    return UserRole.Admin.ToString();
                return UserRole.Editor.ToString(); ;
            }
        }
    }
}
