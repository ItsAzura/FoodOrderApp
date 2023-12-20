using FoodOrderApp.Data.Enum;

namespace FoodOrderApp.ViewModels.Admin
{
    public class EditFoodViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public FoodCategory FoodCategory { get; set; }
    }
}
