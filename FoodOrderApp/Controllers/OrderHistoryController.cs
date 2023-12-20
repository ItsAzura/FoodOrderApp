using FoodOrderApp.Data;
using FoodOrderApp.Models;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _applicationDbContext;

        public OrderHistoryController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var fakeUserId = "e0d5d7f5-71bc-472e-be95-38669cce1849";
            //var loggedInUser = _userManager.FindByIdAsync(fakeUserId).Result;

            var loggedInUser = await _userManager.GetUserAsync(User);


            if (loggedInUser != null)
            {
                OrderHistoryViewModel orderHistoryViewModel = new OrderHistoryViewModel()
                {
                    AppUser = loggedInUser,
                    Order = _applicationDbContext.Orders.Include(e => e.Foods).ThenInclude(od => od.Food).ToList()
                };

                var foodListViewModel = new FoodListViewModel()
                {
                    AppUser = loggedInUser,
                    OrderHistoryViewModel = orderHistoryViewModel                    
                };

                return View(foodListViewModel);
            }

            return View();
        }
    }
}
