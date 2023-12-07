using FoodOrderApp.Data;
using FoodOrderApp.Models;
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
        public IActionResult Index()
        {
            var fakeUserId = "4ae9fe69-982c-4f48-a493-910af488189e";
            var loggedInUser = _userManager.FindByIdAsync(fakeUserId).Result;
            

            if (loggedInUser != null)
            {
                OrderHistoryViewModel orderHistoryViewModel = new OrderHistoryViewModel()
                {
                    AppUser = loggedInUser,
                    Order = _applicationDbContext.Orders.Include(e => e.Foods).ThenInclude(od => od.Food).ToList()
                };

                return View(orderHistoryViewModel);
            }

            return View();
        }
    }
}
