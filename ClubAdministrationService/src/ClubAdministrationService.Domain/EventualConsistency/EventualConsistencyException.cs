using SharedKernel.Results;

namespace ClubAdministrationService.Domain.EventualConsistency;

public sealed class EventualConsistencyException : Exception
{
    public Result? EventualConsistencyError { get; }
    public ICollection<Result>? UnderlyingErrors { get; }

    public EventualConsistencyException(Result eventualConsistencyError, 
        ICollection<Result>? underlyingErrors = null) : base(message: eventualConsistencyError.Error.Description)
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
}