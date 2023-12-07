using FoodOrderApp.Models;

namespace FoodOrderApp.ViewModels
{
    public class OrderHistoryViewModel
    {
        public AppUser AppUser { get; set; }
        public List<Order> Order { get; set; }       
    }
}
