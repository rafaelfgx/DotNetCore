using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DotNetCore.Extensions;

public static class StringExtensions
{
    public static T Deserialize<T>(this string value) where T : class
    {
        return string.IsNullOrWhiteSpace(value)
            ? default
            : JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true
            });
    }

    public static string ExtractNumbers(this string value) => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, "[^0-9]", string.Empty);

    public static bool IsDate(this string value) => !string.IsNullOrWhiteSpace(value) && DateOnly.TryParseExact(value, "dd/MM/yyyy", out _);

    public static bool IsEmail(this string value) => !string.IsNullOrWhiteSpace(value) && MailAddress.TryCreate(value, out _);

    public static bool IsInteger(this string value) => !string.IsNullOrWhiteSpace(value) && BigInteger.TryParse(value, out _);

    public static bool IsLogin(this string value) => !string.IsNullOrWhiteSpace(value) && new Regex("^[a-z0-9._-]{10,50}$").IsMatch(value);

    public static bool IsNumber(this string value) => !string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out _);

    public static bool IsPassword(this string value) => !string.IsNullOrWhiteSpace(value) && new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{10,50}$").IsMatch(value);

    public static bool IsTime(this string value) => !string.IsNullOrWhiteSpace(value) && TimeOnly.TryParseExact(value, "HH:mm:ss", out _);

    public static bool IsUrl(this string value) => !string.IsNullOrWhiteSpace(value) && Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _);

    public static string RemoveAccents(this string value) => string.IsNullOrWhiteSpace(value) ? value : Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(value));

    public static string RemoveDuplicateSpaces(this string value) => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, @"\s+", " ");

    public static string RemoveSpecialCharacters(this string value) => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, "[^0-9a-zA-Z ]+", string.Empty);

    public static string Sanitize(this string value) => string.IsNullOrWhiteSpace(value) ? value : value.RemoveSpecialCharacters().RemoveDuplicateSpaces().RemoveAccents();
}
