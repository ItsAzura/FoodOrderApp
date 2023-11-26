using FoodOrderApp.Data.Enum.EnumAttributes;
using System.Reflection;

namespace FoodOrderApp.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayText(this Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            DisplayTextAttribute attribute =
                (DisplayTextAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayTextAttribute));

            return attribute == null ? enumValue.ToString() : attribute.Text;
        }
    }
}
