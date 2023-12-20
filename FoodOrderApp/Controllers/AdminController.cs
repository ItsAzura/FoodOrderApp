using FoodOrderApp.Data;
using FoodOrderApp.Helpers;
using FoodOrderApp.Interfaces.Admin;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _context;

        public AdminController(IFoodRepository foodRepository, IPhotoService photoService, ApplicationDbContext context)
        {
            this._foodRepository = foodRepository;
            this._photoService = photoService;
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Food(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<Food> foods = await _foodRepository.GetPagingFoods(pageNumber, pageSize);
            IEnumerable<Food> allFoods = await _foodRepository.GetAll();

            int totalPageCount = (int)Math.Ceiling((double)allFoods.Count() / pageSize);

            var pagingFoodViewModel = new PagingFoodViewModel
            {
                Foods = foods,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPageCount
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

            if (foodDetails == null )
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
    }
}
