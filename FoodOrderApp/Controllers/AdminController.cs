using FoodOrderApp.Data;
using FoodOrderApp.Data.Enum;
using FoodOrderApp.Extensions;
using FoodOrderApp.Helpers;
using FoodOrderApp.Interfaces.Admin;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IFoodRepository foodRepository, IPhotoService photoService, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this._foodRepository = foodRepository;
            this._photoService = photoService;
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Counting users
            var userRole = await _context.Roles
                .Where(r => r.Name == "user")
                .FirstOrDefaultAsync();
            var usersWithUserRole = await _context.Users
                .Where(u => _context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == userRole.Id))
                .ToListAsync();
            int userQuantity = usersWithUserRole.Count();
            // Coungting product
            int productQuantity = _context.Foods.Count();
            // Calculating revenue
            decimal revenue = 0;
            var orders = _context.Orders.Include(e => e.Foods).ThenInclude(od => od.Food).ToList();
            foreach (var order in orders)
            {
                if (order.Foods != null)
                {
                    foreach (var orderDetail in order.Foods)
                    {
                        if (orderDetail != null && orderDetail.Food != null)
                        {
                            revenue += orderDetail.Quantity * orderDetail.Food.Price;
                        }
                    }
                }
            }

            AdminHomeViewModel homeVM = new AdminHomeViewModel
            {
                UserQuantity = userQuantity.ToString(),
                ProductQuantity = productQuantity.ToString(),
                Revenue = revenue.ToString()
            };

            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Food(string searchTerm = null, string foodCategory = null, int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Food> allFoods = await _foodRepository.GetAll();

            // Perform search if a search term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Assuming you want to search by food name
                allFoods = allFoods.Where(f => f.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Paging logic
            int totalPageCount = (int)Math.Ceiling((double)allFoods.Count() / pageSize);

            // Apply paging
            IEnumerable<Food> foods = allFoods
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var pagingFoodViewModel = new PagingFoodViewModel
            {
                Foods = foods,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPageCount,
                SearchTerm = searchTerm,
            };

            return View("Food", pagingFoodViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood(CreateFoodViewModel foodViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(foodViewModel.Image);
                var food = new Food
                {
                    Id = FoodIdGenerator.GenerateNextFoodId(_context),
                    Name = foodViewModel.Name,
                    Image = result.Url.ToString(),
                    Description = foodViewModel.Description,
                    Price = foodViewModel.Price,
                    FoodCategory = foodViewModel.Category
                };

                _foodRepository.Add(food);
                return RedirectToAction("Food", "Admin");

                //TempData["ToastTitle"] = "Thông báo";
                //TempData["ToastMessage"] = "Thêm món mới thành công";
                //TempData["ToastType"] = "success";
            }

            TempData["ToastTitle"] = "Thông báo";
            TempData["ToastMessage"] = "Thêm món mới thất bại";
            TempData["ToastType"] = "error";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditFood(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                // Handle the case where id is not provided
                return View("Error");
            }

            var food = await _foodRepository.GetByIdAsync(id);

            if (food == null) return View("Error");

            var foodVM = new EditFoodViewModel
            {
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                FoodCategory = food.FoodCategory,
                URL = food.Image
            };

            return View(foodVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditFood(string id, EditFoodViewModel foodVM)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Failed to edit club");
            //    return View("EditFood", foodVM);
            //}

            var foodEdit = await _foodRepository.GetByIdAsyncNoTracking(id);

            if (foodEdit == null)
            {
                return View("Error");
            }

            string imageAddress = foodEdit.Image;

            if (foodVM.Image != null)
            {
                var photoResult = await _photoService.AddPhotoAsync(foodVM.Image);
                imageAddress = photoResult.Url.ToString();

                _ = _photoService.DeletePhotoAsync(foodEdit.Image);
            }

            var food = new Food
            {
                Id = id,
                Name = foodVM.Name,
                Description = foodVM.Description,
                Image = imageAddress,
                Price = foodVM.Price,
                FoodCategory = foodVM.FoodCategory
            };

            _foodRepository.Update(food);

            return RedirectToAction("Food", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFood(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                // Handle the case where id is not provided
                return View("Error");
            }

            var food = await _foodRepository.GetByIdAsync(id);

            if (food == null) return View("Error");

            var foodVM = new EditFoodViewModel
            {
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                FoodCategory = food.FoodCategory,
                URL = food.Image
            };

            return View(foodVM);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteFood(string id, EditFoodViewModel foodVM)
        {
            var foodDetails = await _foodRepository.GetByIdAsync(id);

            if (foodDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(foodDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(foodDetails.Image);
            }

            _foodRepository.Delete(foodDetails);
            return RedirectToAction("Food", "Admin");
        }

        public IActionResult CustomerOrder()
        {
            var orders = _context.Orders.Include(o => o.Foods).ThenInclude(od => od.Food).ToList();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.OrderStatus = order.OrderStatus == 0 ? OrderStatusCategory.DaXuLy : OrderStatusCategory.DangXuLy;
                _context.Orders.Update(order);
                _context.SaveChanges();

                return RedirectToAction("CustomerOrder", "Admin");
            }

            return View();
        }
    }
}
