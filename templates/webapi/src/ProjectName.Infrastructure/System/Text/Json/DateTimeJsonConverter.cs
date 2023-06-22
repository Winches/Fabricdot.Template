using System.Text.Json.Serialization;
using Fabricdot.Domain.SharedKernel;

namespace System.Text.Json;

public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return reader.GetDateTime();
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options)
    {
        value = value.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(value, SystemClock.Kind) : value;
        writer.WriteStringValue(value.ToUniversalTime());
    }
}
