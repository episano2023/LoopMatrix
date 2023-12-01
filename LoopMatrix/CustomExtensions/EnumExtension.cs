using System.Reflection;

namespace LoopMatrix.CustomExtensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.Name ?? enumValue.ToString();
        }
    }
}