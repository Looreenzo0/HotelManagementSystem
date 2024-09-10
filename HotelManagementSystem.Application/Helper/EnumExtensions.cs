
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelManagementSystem.Application.Helper
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => new SelectListItem
                       {
                           Value = e.ToString(),    // Convert enum to string
                           Text = e.GetDisplayName() // Use helper method for display names
                       });
        }

        // Helper method to get display name for enum values
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                                            .GetField(enumValue.ToString())
                                            .GetCustomAttributes(typeof(DisplayAttribute), false)
                                            .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? enumValue.ToString(); // Fallback to enum name
        }
    }
}
