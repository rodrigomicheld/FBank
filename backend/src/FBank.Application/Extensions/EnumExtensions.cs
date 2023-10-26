using System.ComponentModel;

namespace FBank.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(Enum @enum)
        {
            var description = @enum.GetType().GetField(@enum.ToString());

            var attributes = (DescriptionAttribute[])description.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;

            return @enum.ToString();
        }
    }
}
