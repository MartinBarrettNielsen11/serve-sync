using SharedKernel.Results;

namespace ClubAdministrationService.Domain.EventualConsistency;

internal static class EventualConsistencyError
{
	public const int EventualConsistencyType = 100;

	public static Result From(string code, string description)
	{
		// maybe add a separate error type for eventual consistency errors
		return Result.Failure(Error.Failure(code, description));
	}
}