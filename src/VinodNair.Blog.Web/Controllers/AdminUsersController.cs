using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinodNair.Blog.Web.ViewModels;
using VinodNair.Blog.Domain.Entities;
using VinodNair.Blog.Domain.Abstractions;
using System.Linq;
using VinodNair.Blog.Domain.Enums;

namespace VinodNair.Blog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUsersController(IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // TODO : Implement pagination
            var users = await userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email,
                    FullName = user.FullName,
                    Roles = string.Join(",", user.UserRoles.Select(x => x.Role.Name))
                });
            }

            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName,
            };


            var identityResult =
                await userManager.CreateAsync(applicationUser, request.Password);

            if (identityResult is not null)
            {
                if (identityResult.Succeeded)
                {
                    // assign roles to this user
                    var roles = new List<string> { UserRole.Editor.ToString() };

                    if (request.IsAdmin)
                    {
                        roles.Add(UserRole.Admin.ToString());
                    }

                    identityResult = await userManager.AddToRolesAsync(applicationUser, roles);


                    if (identityResult is not null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }

                }
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }

            return View();
        }
    }
}
