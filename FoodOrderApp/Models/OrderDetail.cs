using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class OrderDetail
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("Food")]
        public string FoodId { get; set; }
        public Food Food { get; set; }

        [NotMapped]
        public decimal Total => Quantity * Food.Price;
    }
}
