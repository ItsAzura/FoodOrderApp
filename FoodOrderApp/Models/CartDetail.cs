using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class CartDetail
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string? Noted { get; set; }
        [ForeignKey("Cart")]
        public string CartId { get; set; }
        public Cart Cart { get; set; }
        [ForeignKey("Food")]
        public string FoodId { get; set; }
        public Food Food { get; set; }

        [NotMapped]
        public decimal Total => Quantity * Food.Price;
    }
}
