using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCore.Extensions;

public static class ObjectExtensions
{
    public static byte[] Bytes(this object obj) => Encoding.Default.GetBytes(JsonSerializer.Serialize(obj));

    public static Dictionary<string, object> Dictionary(this object obj)
    {
        if (obj is null) return default;

        var dictionary = new Dictionary<string, object>();

        foreach (var property in obj.GetType().GetProperties())
        {
            dictionary[property.Name] = property.GetValue(obj);
        }

        return dictionary;
    }

    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(this object obj) where T : Attribute => obj.GetType().GetProperties().Where(property => Attribute.IsDefined(property, typeof(T)));

    public static string Serialize(this object obj) => JsonSerializer.Serialize(obj, new JsonSerializerOptions(JsonSerializerDefaults.Web) { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

    public static void SetProperty(this object obj, string name, object value) => obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)?.SetValue(obj, value);
}
