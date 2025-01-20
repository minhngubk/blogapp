using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VinodNair.Blog.Domain.Entities;
using VinodNair.Blog.Web.ViewModels;

namespace VinodNair.Blog.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email
                };

                // Check user exist
                var existingdUser = await userManager.FindByEmailAsync(applicationUser.Email);
                if(existingdUser != null)
                {
                    ViewData["ErrorMessage"] = "Email already exists!";
                    return View(registerViewModel);
                }

                var identityResult = await userManager.CreateAsync(applicationUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    // assign this user the "Editor" role
                    var roleIdentityResult = await userManager.AddToRoleAsync(applicationUser, "Editor");

                    if (roleIdentityResult.Succeeded)
                    {
                        ViewData["SuccessMessage"] = "Congratulations, you have successfully registered an account!";                      
                        return RedirectToAction("Register");
                    }
                }
                var errorMessage = identityResult.Errors?.FirstOrDefault()?.Description;
                var defaultErrorMessage = "An error occurred, please contact the administrator!";
                ViewData["ErrorMessage"] = !string.IsNullOrEmpty(errorMessage) ? errorMessage : defaultErrorMessage;
                return View(registerViewModel);
            }

            // Show error notification
            return View(registerViewModel);
        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            ViewData["ErrorMessage"] = "User name or password is incorrect";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
