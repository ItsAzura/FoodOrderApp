using FoodOrderApp.Data.Enum;

namespace FoodOrderApp.Models
{
    public class Food
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public FoodCategory foodCategory { get; set; }
        public virtual ICollection<OrderDetail> Orders { get; set; }
        public virtual ICollection<CartDetail> Carts { get; set; }
    }
}
