using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Infrastructure;

internal static class ProblemDetailsMapper
{
	// Assumes correct usage - do not supply it with something that is not an error
	public static IResult Problem(List<Error> errors)
	{
		if (errors.Count is 0) return Results.Problem();

		if (errors.TrueForAll(e => e.Type == ErrorType.Validation)) return ValidationProblem(errors);

		return Problem(errors[0]);
	}

	private static IResult ValidationProblem(List<Error> errors)
	{
		Dictionary<string, string[]> validationErrors = errors
			.SelectMany(e =>
			{
				if (e is ValidationError validationError) return validationError.Errors;

				return [e];
			})
			.GroupBy(e => e.Code, StringComparer.Ordinal)
			.ToDictionary(
				group => group.Key,
				group => group
					.Select(error => error.Description)
					.Distinct(StringComparer.Ordinal)
					.ToArray(),
				StringComparer.Ordinal);

		return Results.ValidationProblem(validationErrors);
	}

	private static IResult Problem(Error error)
	{
		return Results.Problem(title: GetTitle(error),
			detail: GetDetail(error),
			type: GetType(error.Type),
			statusCode: GetStatusCode(error.Type));
	}

	private static string GetTitle(Error error)
	{
		return error.Type switch
		{
			ErrorType.Validation => "Validation error",
			ErrorType.Problem => error.Code,
			ErrorType.NotFound => error.Code,
			ErrorType.Conflict => error.Code,
			_ => "Server failure"
		};
	}

	private static string GetDetail(Error error)
	{
		return error.Type switch
		{
			ErrorType.Validation => error.Description,
			ErrorType.Problem => error.Description,
			ErrorType.NotFound => error.Description,
			ErrorType.Conflict => error.Description,
			_ => "An unexpected error occurred."
		};
	}

	private static string GetType(ErrorType errorType)
	{
		return errorType switch
		{
			ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
			ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
			ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
			ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
			_ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
		};
	}

	private static int GetStatusCode(ErrorType errorType)
	{
		return errorType switch
		{
			ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			_ => StatusCodes.Status500InternalServerError
		};
	}
}