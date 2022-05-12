using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DotNetCore.Extensions;

public static class StringExtensions
{
    public static T Deserialize<T>(this string value) where T : class
    {
        if (string.IsNullOrWhiteSpace(value)) return default;

        return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true
        });
    }

    public static string LowerCamelCase(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value[0].ToString().ToLowerInvariant() + value[1..];
    }

    public static string NonSpecialCharacters(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var bytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(value);

        value = Encoding.Default.GetString(bytes);

        return new Regex("[^0-9a-zA-Z._ ]+?").Replace(value, string.Empty);
    }

    public static string NumericCharacters(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, "[^0-9]", string.Empty);
    }

    public static string RemoveExtraSpaces(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"\s+", " ");
    }
}
