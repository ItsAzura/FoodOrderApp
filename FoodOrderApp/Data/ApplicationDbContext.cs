using FoodOrderApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Food> Foods;
        public DbSet<Order> Orders;
        public DbSet<AppUser> AppUsers;
        public DbSet<Cart> Carts;
    }
}
