using Microsoft.AspNetCore.Identity;

namespace FoodOrderApp.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}
