using FoodOrderApp.Data;
using FoodOrderApp.Data.Enum;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FoodOrderApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;

namespace FoodOrderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public int PageSize = 8;

        public HomeController(ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, decimal? minPrice, decimal? maxPrice, int foodPage = 1, string searchTerm = "")
        {
            #region "Hien Thi, Phan Trang"

            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            IQueryable<Food> foodsQuery;
            switch (sortOrder)
            {
                case "asc":
                    foodsQuery = _applicationDbContext.Foods.OrderBy(f => f.Price);
                    break;
                case "desc":
                    foodsQuery = _applicationDbContext.Foods.OrderByDescending(f => f.Price);
                    break;
                default:
                    foodsQuery = _applicationDbContext.Foods;
                    break;
            }

            if (minPrice.HasValue)
            {
                foodsQuery = foodsQuery.Where(f => f.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                foodsQuery = foodsQuery.Where(f => f.Price <= maxPrice.Value);
            }


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTermLower = searchTerm.ToLower();
                foodsQuery = foodsQuery.Where(f => EF.Functions.Like(f.Name.ToLower(), "%" + searchTermLower + "%"));
            }


            decimal? maxDisplayedPrice = foodsQuery.Max(f => (decimal?)f.Price);
            decimal? minDisplayedPrice = foodsQuery.Min(f => (decimal?)f.Price);

            var foods = foodsQuery
                .Skip((foodPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            #endregion

            #region "Cart"
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                //CartUserViewModel cartUserViewModel = new CartUserViewModel()
                //{
                //    AppUser = user,
                //    ApplicationDbContext = _applicationDbContext,
                //    Carts = _applicationDbContext.Carts.Include(e => e.Foods).ToList(),
                //};

                /*return View(cartUserViewModel)*/;

                var cartDetails = _applicationDbContext.Carts
                .SelectMany(e => e.Foods)
                .Include(cd => cd.Food).ToList();

                return View
                (
                    new FoodListViewModel
                    {
                        AppUser = user,
                        ApplicationDbContext = _applicationDbContext,
                        Carts = _applicationDbContext.Carts.Include(e => e.Foods).ToList(),
                        CartDetails = cartDetails,
                        Foods = foods,
                        PagingInfo = new PagingInfo
                        {
                            ItemsPerPage = 8,
                            CurrentPage = foodPage,
                            TotalItems = foodsQuery.Count(),
                            SortOrder = sortOrder
                        }                     
                    }
                );
            }

            return View();
            #endregion
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