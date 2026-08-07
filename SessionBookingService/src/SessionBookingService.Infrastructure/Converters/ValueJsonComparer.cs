using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SessionBookingService.Infrastructure.Converters;

internal sealed class ValueJsonComparer<T>() : ValueComparer<T>((l, r) =>
		JsonSerializer.Serialize(l, JsonSerializerOptions.Default) ==
		JsonSerializer.Serialize(r, JsonSerializerOptions.Default),
	v => object.Equals(v, default(T))
		? 0
		: StringComparer.Ordinal.GetHashCode(JsonSerializer.Serialize(v, JsonSerializerOptions.Default)),
	v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
		JsonSerializerOptions.Default)!);
