using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Extensions;

internal static class AssertLoggingExtensions
{
	public static void ShouldHaveInformationLog(
		this FakeLogCollector collector,
		string messageTemplate,
		params (string Key, string Value)[] properties)
	{
		IReadOnlyList<FakeLogRecord> logs = collector.GetSnapshot();

		(string Key, string Value)[] expectedProperties =
		[
			("{OriginalFormat}", messageTemplate),
			.. properties
		];

		FakeLogRecord? matchingLog = logs
			.Where(log => log.Level == LogLevel.Information)
			.FirstOrDefault(log =>
				log.StructuredState is not null &&
				expectedProperties.All(property =>
					HasProperty(log, property.Key, property.Value)));

		Assert.NotNull(matchingLog);
	}

	private static bool HasProperty(FakeLogRecord log, string key, string expectedValue)
	{
		return log.StructuredState?.Any(kvp =>
			string.Equals(kvp.Key, key, StringComparison.OrdinalIgnoreCase) &&
			string.Equals(kvp.Value, expectedValue, StringComparison.OrdinalIgnoreCase)) == true;
	}
}
