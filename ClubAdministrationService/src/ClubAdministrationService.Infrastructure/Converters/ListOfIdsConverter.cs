using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClubAdministrationService.Infrastructure.Converters;

public sealed class ListOfIdsConverter(ConverterMappingHints? mappingHints = null)
	: ValueConverter<List<Guid>, string>(v => string.Join(',', v),
										v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
											.Select(Guid.Parse)
											.ToList(),
										mappingHints);
