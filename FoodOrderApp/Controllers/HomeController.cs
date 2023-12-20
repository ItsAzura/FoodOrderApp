using FoodOrderApp.Data;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            //var fakeUserId = "ae8d85f9-7ea8-4f85-8664-d5f344ff5655";

            var loggedInUser = _userManager.FindByIdAsync(fakeUserId).Result;

            if (loggedInUser != null)
            {

                CartUserViewModel cartUserViewModel = new CartUserViewModel()
                {
                    AppUser = loggedInUser,
                    ApplicationDbContext = _applicationDbContext,
                    Carts = _applicationDbContext.Carts.Include(e => e.Foods).ToList(),

                };

                return View(cartUserViewModel);
            }

            return View();
        }

        [HttpPost]
        public IActionResult UpdateCartDetailQuantity([FromBody] CartDetailQuantityUpdate model)
        {
            var cartDetail = _applicationDbContext.Carts
                .SelectMany(e => e.Foods)
                .Include(cd => cd.Food)
                .SingleOrDefault(cd => cd.Id == model.CartDetailId);

            if (cartDetail != null)
            {
                cartDetail.Quantity = model.UpdatedQuantity;

                _applicationDbContext.SaveChanges();

                Console.WriteLine($"Updated CartDetailId: {cartDetail.Id}");
            }
            else
            {
                Console.WriteLine($"CartDetail with ID {model.CartDetailId} not found.");
            }

            return Json(new { success = true });
        }

        public class CartDetailQuantityUpdate
        {
            public string CartDetailId { get; set; }
            public int UpdatedQuantity { get; set; }
        }

        [HttpPost]
        public IActionResult DeleteCartDetail([FromBody] CartDetailDelete model)
        {            
            var cartDetail = _applicationDbContext.Carts
                .SelectMany(c => c.Foods)
                .FirstOrDefault(cd => cd.Id == model.CartDetailId);

            if (cartDetail != null)
            {
                _applicationDbContext.Remove(cartDetail);
                _applicationDbContext.SaveChanges();

                return Json(new { success = true, message = "CartDetail deleted successfully." });
            }

            return Json(new { success = false, message = "CartDetail not found." });
        }

        public class CartDetailDelete
        {
            public string CartDetailId { get; set; }
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


        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}