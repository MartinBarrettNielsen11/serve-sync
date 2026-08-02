namespace SharedKernel.Results;

public sealed record ValidationError(Error[] Errors) :
	Error("Validation.General", "One or more errors occured", ErrorType.Validation)
{
	public Error[] Errors { get; } = Errors;

	public static ValidationError FromResult(IEnumerable<Result> results)
	{
		Error[] errors = results.Where(r => r.IsFailure)
			.Select(r => r.Error)
			.ToArray();

		ValidationError validationError = new(errors);

		return validationError;
	}
}
