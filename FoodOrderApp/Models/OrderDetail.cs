using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("Food")]
        public string FoodId { get; set; }
        public Food Food { get; set; }
    }
}
