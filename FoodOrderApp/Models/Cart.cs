using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderApp.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<CartDetail> Foods { get; set; }
    }
}
