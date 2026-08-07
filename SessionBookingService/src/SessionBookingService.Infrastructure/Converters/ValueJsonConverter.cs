using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SessionBookingService.Infrastructure.Converters;

public sealed class ValueJsonConverter<T>(ConverterMappingHints? mappingHints = null)
	: ValueConverter<T, string>(v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
								v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions.Default)!,
								mappingHints);
