namespace SharedKernel.Results;

public record Error(string Code, string Description, ErrorType Type, string? StackTrace = null)
{
	public static readonly Error None = new(string.Empty,
											string.Empty,
											ErrorType.Failure);

	public static readonly Error NullValue = new("General.Null",
												"Null value was provided",
												ErrorType.Failure);

	public string Code { get; set; } = Code;
	public string Description { get; set; } = Description;
	public ErrorType Type { get; set; } = Type;
	public string? StackTrace { get; set; } = StackTrace;

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
