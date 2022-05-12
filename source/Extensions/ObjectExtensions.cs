using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCore.Extensions;

public static class ObjectExtensions
{
    public static byte[] Bytes(this object obj)
    {
        return Encoding.Default.GetBytes(JsonSerializer.Serialize(obj));
    }

    public static string Serialize(this object obj)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(obj, options);
    }
}
