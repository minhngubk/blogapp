using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VinodNair.Blog.Domain.Abstractions;
using VinodNair.Blog.Infrastructure.Data.Repositories;
using VinodNair.Blog.Web.ViewModels;

namespace VinodNair.Blog.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var model = new HomeViewModel();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
