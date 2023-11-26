namespace FoodOrderApp.Models
{
    public class AppUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
