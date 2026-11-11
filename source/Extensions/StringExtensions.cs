using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DotNetCore.Extensions;

public static class StringExtensions
{
    extension(string value)
    {
        public T Deserialize<T>() where T : class
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true
                });
        }

        public string ExtractNumbers() => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, "[^0-9]", string.Empty);

        public bool IsDate() => !string.IsNullOrWhiteSpace(value) && DateOnly.TryParseExact(value, "dd/MM/yyyy", out _);

        public bool IsEmail() => !string.IsNullOrWhiteSpace(value) && MailAddress.TryCreate(value, out _);

        public bool IsInteger() => !string.IsNullOrWhiteSpace(value) && BigInteger.TryParse(value, out _);

        public bool IsLogin() => !string.IsNullOrWhiteSpace(value) && new Regex("^[a-z0-9._-]{10,50}$").IsMatch(value);

        public bool IsNumber() => !string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out _);

        public bool IsPassword() => !string.IsNullOrWhiteSpace(value) && new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{10,50}$").IsMatch(value);

        public bool IsTime() => !string.IsNullOrWhiteSpace(value) && TimeOnly.TryParseExact(value, "HH:mm:ss", out _);

        public bool IsUrl() => !string.IsNullOrWhiteSpace(value) && Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _);

        public string RemoveAccents() => string.IsNullOrWhiteSpace(value) ? value : Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(value));

        public string RemoveDuplicateSpaces() => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, @"\s+", " ");

        public string RemoveSpecialCharacters() => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, "[^0-9a-zA-Z ]+", string.Empty);

        public string Sanitize() => string.IsNullOrWhiteSpace(value) ? value : value.RemoveSpecialCharacters().RemoveDuplicateSpaces().RemoveAccents();
    }
}
