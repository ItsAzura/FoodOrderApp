using FoodOrderApp.Data;
using FoodOrderApp.Models;

namespace FoodOrderApp.ViewModels
{
    public class CartUserViewModel
    {
        public ApplicationDbContext ApplicationDbContext { get; set; }
        public List<Cart> Carts { get; set; }
        public AppUser AppUser { get; set; }
    }
}
