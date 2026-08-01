namespace SharedKernel.Results;

public sealed record Error
{
	public static readonly Error None = new(
		string.Empty,
		string.Empty,
		ErrorType.Failure);

	public static readonly Error NullValue = new(
		"General.Null",
		"Null value was provided",
		ErrorType.Failure);

	public Error(string code, string description, ErrorType type, string? stackTrace = null)
	{
		Code = code;
		Description = description;
		Type = type;
		StackTrace = stackTrace;
	}

	public string Code { get; set; }
	public string Description { get; set; }
	public ErrorType Type { get; set; }
	public string? StackTrace { get; set; }

	public static Error Failure(string code, string description)
	{
		return new Error(code, description, ErrorType.Failure);
	}

	public static Error NotFound(string code, string description)
	{
		return new Error(code, description, ErrorType.NotFound);
	}

	public static Error Problem(string code, string description)
	{
		return new Error(code, description, ErrorType.Problem);
	}

	public static Error Conflict(string code, string description)
	{
		return new Error(code, description, ErrorType.Conflict);
	}

	// Intended usage pattern:
	//try
	//{
	//    // something risky
	//}
	//catch (Exception ex)
	//{
	//    return Error.Unexpected("ERR001", "Unexpected failure", ex);
	//}
	// or as handling in global handling/filter
	public static Error Unexpected(string code, string description)
	{
		return new Error(code, description, ErrorType.Unexpected, Environment.StackTrace);
	}
}
