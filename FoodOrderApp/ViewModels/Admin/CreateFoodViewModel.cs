using FoodOrderApp.Data.Enum;

namespace FoodOrderApp.ViewModels.Admin
{
    public class CreateFoodViewModel
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public FoodCategory Category { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }
}
