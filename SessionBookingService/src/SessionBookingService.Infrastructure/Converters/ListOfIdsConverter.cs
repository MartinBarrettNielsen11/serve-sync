using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SessionBookingService.Infrastructure.Converters;

public sealed class ListOfIdsConverter : ValueConverter<List<Guid>, string>
{
	public ListOfIdsConverter(ConverterMappingHints? mappingHints = null)
		: base(
			v => string.Join(',', v),
			v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList(),
			mappingHints)
	{
	}
}
