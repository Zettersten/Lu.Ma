using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lu.Ma.Serialization;

internal sealed class ISO8601DurationConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return System.Xml.XmlConvert.ToTimeSpan(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(System.Xml.XmlConvert.ToString(value));
    }
}