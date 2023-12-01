using FoodOrderApp.Data;
using FoodOrderApp.Models;

namespace FoodOrderApp.ViewModels
{
    public class CartUserViewModel
    {
        public List<Cart> Carts { get; set; }
        public List<Food> Foods { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public AppUser AppUser { get; set; }
    }
}
