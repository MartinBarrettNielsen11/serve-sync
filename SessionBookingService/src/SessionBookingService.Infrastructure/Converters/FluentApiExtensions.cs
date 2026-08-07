using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SessionBookingService.Infrastructure.Converters;

public static class FluentApiExtensions
{
	// Look into calling .ToJson() on the owned instances instead, if Postgress offers support for JSON columns
	public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> propertyBuilder)
	{
		return propertyBuilder.HasConversion(
			new ValueJsonConverter<T>(),
			new ValueJsonComparer<T>());
	}

	public static PropertyBuilder<T> HasListOfIdsConverter<T>(this PropertyBuilder<T> propertyBuilder)
	{
		return propertyBuilder.HasConversion(
			new ListOfIdsConverter(),
			new ListOfIdsComparer());
	}
}
