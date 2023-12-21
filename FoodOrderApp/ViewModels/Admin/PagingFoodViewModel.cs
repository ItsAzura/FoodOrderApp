using FoodOrderApp.Models;

namespace FoodOrderApp.ViewModels.Admin
{
    public class PagingFoodViewModel
    {
        public IEnumerable<Food> Foods { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string? SearchTerm { get; set; }
    }
}
