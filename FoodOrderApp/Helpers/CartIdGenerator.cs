using FoodOrderApp.Data;
using FoodOrderApp.Models;

namespace FoodOrderApp.Helpers
{
    public class CartIdGenerator
    {
        public static string GenerateNextCartId(ApplicationDbContext context, AppUser user)
        {
            // Get the highest existing Cart ID for the given user
            string highestId = context.Carts
                .Where(c => c.AppUserId == user.Id)
                .Select(c => c.Id)
                .OrderByDescending(id => id)
                .FirstOrDefault();

            // Increment the number part of the ID
            string newNumberPart = GetNextNumberPart(highestId);

            // Format the new ID
            string newId = $"CA{newNumberPart}";

            return newId;
        }

        private static string GetNextNumberPart(string id)
        {
            // Extract the numeric part from the ID
            string numberPart = id?.Substring(1);

            if (int.TryParse(numberPart, out int number))
            {
                // Increment the numeric part
                number++;

                // Convert back to a string with leading zeros
                return number.ToString("D3");
            }

            // Default to "001" if parsing fails
            return "001";
        }
    }
}
