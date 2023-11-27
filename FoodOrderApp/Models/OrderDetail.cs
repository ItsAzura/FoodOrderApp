using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class OrderDetail
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public Food Food { get; set; }

        [NotMapped]
        public decimal Total => Quantity * Food.Price;
    }
}
