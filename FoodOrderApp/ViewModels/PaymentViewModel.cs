using FoodOrderApp.Models;

namespace FoodOrderApp.ViewModels
{
    public class PaymentViewModel
    {
        public Cart CartUser { get; set; }
        public List<CartDetail> CartUserDetails { get; set; }
        public Order OrderUser { get; set; }
    }
}
