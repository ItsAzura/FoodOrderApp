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
            }

            return PartialView("~/Views/Partial Views/Admin/_ModalAddFood.cshtml", foodViewModel);
        }
    }
}
