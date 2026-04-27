using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCore.Extensions;

public static class ObjectExtensions
{
    extension(object obj)
    {
        public byte[] Bytes() => Encoding.Default.GetBytes(JsonSerializer.Serialize(obj));

        public Dictionary<string, object> Dictionary()
        {
            if (obj is null) return null;

            var dictionary = new Dictionary<string, object>();

            foreach (var property in obj.GetType().GetProperties())
            {
                dictionary[property.Name] = property.GetValue(obj);
            }

            return dictionary;
        }

        public IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>() where T : Attribute => obj.GetType().GetProperties().Where(property => Attribute.IsDefined(property, typeof(T)));

        public string Serialize() => JsonSerializer.Serialize(obj, new JsonSerializerOptions(JsonSerializerDefaults.Web) { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        public void SetProperty(string name, object value) => obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)?.SetValue(obj, value);
    }
}
