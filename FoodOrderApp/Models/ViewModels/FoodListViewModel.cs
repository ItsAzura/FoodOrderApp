using FoodOrderApp.Data;
using FoodOrderApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models.ViewModels
{
    public class FoodListViewModel
    {
        public IEnumerable<Food> Foods { get; set; } = Enumerable.Empty<Food>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public string? CurrentCategory { get; set; }

        #region "Quang"
        public ApplicationDbContext ApplicationDbContext { get; set; }
        public List<Cart> Carts { get; set; }
        public List<CartDetail> CartDetails { get; set; }
        public AppUser AppUser { get; set; }
        public PaymentViewModel PaymentViewModel { get; set; }
        public OrderHistoryViewModel OrderHistoryViewModel { get; set; }
        #endregion

        #region "Nhat"        
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        #endregion
    }
}