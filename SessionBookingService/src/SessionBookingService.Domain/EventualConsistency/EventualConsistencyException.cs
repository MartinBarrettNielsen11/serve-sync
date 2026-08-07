using SharedKernel.Results;

namespace SessionBookingService.Domain.EventualConsistency;

public sealed class EventualConsistencyException : Exception
{
	public EventualConsistencyException(Result eventualConsistencyError,
										ICollection<Result>? underlyingErrors = null) : base(eventualConsistencyError
																							.Error.Description)
	{
		EventualConsistencyError = eventualConsistencyError;
		UnderlyingErrors = underlyingErrors ?? [];
	}

	public EventualConsistencyException()
	{
	}

	public EventualConsistencyException(string message) : base(message)
	{
	}

	public EventualConsistencyException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public Result? EventualConsistencyError { get; }
	public ICollection<Result>? UnderlyingErrors { get; }
}
