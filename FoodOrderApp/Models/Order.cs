using FoodOrderApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class Order
    {
        public string? Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public OrderStatusCategory? OrderStatus { get; set; }
        public FormDeliveryCategory? FormDelivery { get; set; }
        public string? Receiver { get; set; }
        public string? Location { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public virtual ICollection<OrderDetail>? Foods { get; set; }
    }
}
