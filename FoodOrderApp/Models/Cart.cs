using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class Cart
    {
        [ForeignKey("AppUser")]
        public string Id { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<CartDetail> Foods { get; set; }
    }
}
