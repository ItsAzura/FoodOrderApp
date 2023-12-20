using FoodOrderApp.Data;
using FoodOrderApp.Data.Enum;
using FoodOrderApp.Models;
using FoodOrderApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;

namespace FoodOrderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int PageSize = 8;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string sortOrder, decimal? minPrice, decimal? maxPrice, int foodPage = 1, string searchTerm = "")
        {
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            IQueryable<Food> foodsQuery;
            switch (sortOrder)
            {
                case "asc":
                    foodsQuery = _context.Foods.OrderBy(f => f.Price);
                    break;
                case "desc":
                    foodsQuery = _context.Foods.OrderByDescending(f => f.Price);
                    break;
                default:
                    foodsQuery = _context.Foods;
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

            return View
                (
                new FoodListViewModel
                {
                    Foods = foods,
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = foodPage,
                        TotalItems = foodsQuery.Count(),
                        SortOrder = sortOrder
                    }
                }
                );
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