using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCore.AspNetCore;

public class JsonStringBoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => bool.TryParse(reader.GetString(), out var result) && result;

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
}
