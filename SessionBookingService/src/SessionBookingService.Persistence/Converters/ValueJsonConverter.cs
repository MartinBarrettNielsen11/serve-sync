using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SessionBookingService.Persistence.Converters;

public class ValueJsonConverter<T> : ValueConverter<T, string>
{
    public ValueJsonConverter(ConverterMappingHints? mappingHints = null)
        : base(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions.Default)!,
            mappingHints)
    {
    }
}