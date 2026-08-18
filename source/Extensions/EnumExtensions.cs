using System.ComponentModel;

namespace DotNetCore.Extensions;

public static class EnumExtensions
{
    extension(Enum value)
    {
        public string GetDescription()
        {
            if (value is null) return null;

            var attribute = value.GetAttribute<DescriptionAttribute>();

            return attribute is null ? value.ToString() : attribute.Description;
        }

        public string[] ToArray() => value?.ToString().Split(", ");

        private T GetAttribute<T>() where T : Attribute
        {
            if (value is null) return null;

            var member = value.GetType().GetMember(value.ToString());

            var attributes = member[0].GetCustomAttributes(typeof(T), false);

            return (T)attributes[0];
        }
    }
}
