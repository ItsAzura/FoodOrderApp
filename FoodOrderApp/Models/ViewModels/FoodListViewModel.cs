namespace FoodOrderApp.Models.ViewModels
{
    public class FoodListViewModel
    {
        public IEnumerable<Food> Foods { get; set; } = Enumerable.Empty<Food>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public string? CurrentCategory { get; set; }
    }
}
