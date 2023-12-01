using FoodOrderApp.Data;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodOrderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fakeUserId = "05657dcf-6ee9-4336-a7f8-239f99479b75";
            var loggedInUser = _userManager.FindByIdAsync(fakeUserId).Result;

            if (loggedInUser != null) {

                CartUserViewModel cartUserViewModel = new CartUserViewModel()
                {
                    AppUser = loggedInUser,
                    Carts = _applicationDbContext.Carts.ToList(),  
                    Foods = _applicationDbContext.Foods.ToList(),
                };

                return View(cartUserViewModel);
            }

            return View();            
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