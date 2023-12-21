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

namespace FoodOrderApp.Controllers
{

    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int PageSize = 8;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index(int foodPage = 1)
        {
            var applicationDbcontext = _context.Foods
                .Skip((foodPage - 1) * PageSize)
                .Take(PageSize);
            return View(await applicationDbcontext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
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
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
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
                    _context.Update(food);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
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
            if (_context.Foods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
            }
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(string id)
        {
          return (_context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Foods/Category/MonChay
        public async Task<IActionResult> Category(FoodCategory foodCategory, string sortOrder, decimal? minPrice, decimal? maxPrice, int foodPage = 1)
        {
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;

            if (_context.Foods == null)
            {
                return Problem("null");
            }


            IQueryable<Food> foodsQuery;
            switch (sortOrder)
            {
                case "asc":
                    foodsQuery = _context.Foods.Where(f => f.FoodCategory == foodCategory).OrderBy(f => f.Price);
                    break;
                case "desc":
                    foodsQuery = _context.Foods.Where(f => f.FoodCategory == foodCategory).OrderByDescending(f => f.Price);
                    break;
                default:
                    foodsQuery = _context.Foods.Where(f => f.FoodCategory == foodCategory);
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

            return View
                (
                new FoodListViewModel
                {
                    CurrentCategory = foodCategory.ToString(),
                    Foods = foods,
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = foodPage,
                        TotalItems = totalItems,
                        SortOrder = sortOrder
                    }
                }
                );
        }


    }
}
 