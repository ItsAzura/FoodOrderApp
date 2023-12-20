using FoodOrderApp.Data;

namespace FoodOrderApp.Helpers
{
    public class FoodIdGenerator
    {
        public static string GenerateNextFoodId(ApplicationDbContext context)
        {
            // Get the highest existing Food ID
            string highestId = context.Foods
                .Select(f => f.Id)
                .OrderByDescending(id => id)
                .FirstOrDefault();

            // Increment the number part of the ID
            string newNumberPart = GetNextNumberPart(highestId);

            // Format the new ID
            string newId = $"F{newNumberPart}";

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
