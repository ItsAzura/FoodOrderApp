using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodOrderApp.Data;
using FoodOrderApp.Models;
using FoodOrderApp.Data.Enum;
using FoodOrderApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderApp.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public int PageSize = 8;

        public FoodsController(ApplicationDbContext applicationDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Foods
        public async Task<IActionResult> Index(int foodPage = 1)
        {
            var applicationDbcontext = _applicationDbContext.Foods
                .Skip((foodPage - 1) * PageSize)
                .Take(PageSize);
            return View(await applicationDbcontext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _applicationDbContext.Foods == null)
            {
                return NotFound();
            }

            var food = await _applicationDbContext.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Description,Price,FoodCategory")] Food food)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Add(food);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _applicationDbContext.Foods == null)
            {
                return NotFound();
            }

            var food = await _applicationDbContext.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Image,Description,Price,FoodCategory")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _applicationDbContext.Update(food);
                    await _applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _applicationDbContext.Foods == null)
            {
                return NotFound();
            }

            var food = await _applicationDbContext.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_applicationDbContext.Foods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
            }
            var food = await _applicationDbContext.Foods.FindAsync(id);
            if (food != null)
            {
                _applicationDbContext.Foods.Remove(food);
            }

            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(string id)
        {
            return (_applicationDbContext.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Foods/Category/MonChay
        public async Task<IActionResult> Category(FoodCategory foodCategory, string sortOrder, decimal? minPrice, decimal? maxPrice, int foodPage = 1)
        {
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            if (_applicationDbContext.Foods == null)
            {
                return Problem("null");
            }


            IQueryable<Food> foodsQuery;
            switch (sortOrder)
            {
                case "asc":
                    foodsQuery = _applicationDbContext.Foods.Where(f => f.FoodCategory == foodCategory).OrderBy(f => f.Price);
                    break;
                case "desc":
                    foodsQuery = _applicationDbContext.Foods.Where(f => f.FoodCategory == foodCategory).OrderByDescending(f => f.Price);
                    break;
                default:
                    foodsQuery = _applicationDbContext.Foods.Where(f => f.FoodCategory == foodCategory);
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

            decimal? maxDisplayedPrice = await foodsQuery.MaxAsync(f => (decimal?)f.Price);
            decimal? minDisplayedPrice = await foodsQuery.MinAsync(f => (decimal?)f.Price);
            int totalItems = await foodsQuery.CountAsync();

            var foods = await foodsQuery
                .Skip((foodPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

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

                /*return View(cartUserViewModel)*/
                ;

                var cartDetails = _applicationDbContext.Carts
                .SelectMany(e => e.Foods)
                .Include(cd => cd.Food).ToList();

                return View
                (
                    new FoodListViewModel
                    {
                        CurrentCategory = foodCategory.ToString(),
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
    
    } }
 