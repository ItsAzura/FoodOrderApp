using FoodOrderApp.Data;
using FoodOrderApp.Interfaces.Admin;
using FoodOrderApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Repository.Admin
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Food>> GetAll()
        {
            return await _context.Foods.ToListAsync();
        }
        public async Task<IEnumerable<Food>> GetPagingFoods(int pageNumber, int pageSize)
        {
            return await _context.Foods
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Food> GetByIdAsync(string id)
        {
            return await _context.Foods.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Food> GetByIdAsyncNoTracking(string id)
        {
            return await _context.Foods.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Add(Food food)
        {
            _context.Add(food);
            return Save();
        }

        public bool Delete(Food food)
        {
            _context.Remove(food);
            return Save();
        }

        public bool Update(Food food)
        {
            _context.Update(food);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
