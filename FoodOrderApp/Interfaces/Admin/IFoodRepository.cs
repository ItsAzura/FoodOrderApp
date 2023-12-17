using FoodOrderApp.Models;

namespace FoodOrderApp.Interfaces.Admin
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetPagingFoods(int pageNumber, int pageSize);
        Task<IEnumerable<Food>> GetAll();
        Task<Food> GetByIdAsync(string id);
        bool Add(Food food);
        bool Update(Food food);
        bool Delete(Food food);
        bool Save();
    }
}
