using System.Diagnostics.CodeAnalysis;

// ReSharper disable ArrangeRedundantParentheses

namespace SharedKernel.Results;

public class Result
{
	protected Result(bool isSuccess, Error error)
	{
		var hasInvalidErrorState = (isSuccess && error != Error.None) ||
									(!isSuccess && error == Error.None);

		if (hasInvalidErrorState) throw new ArgumentException("Invalid error", nameof(error));

		IsSuccess = isSuccess;
		Error = error;
	}

	public bool IsSuccess { get; }
	public Error Error { get; }

	public bool IsFailure => !IsSuccess;

	public static Result Success()
	{
		return new Result(true, Error.None);
	}

	public static Result<TValue> Success<TValue>(TValue value)
	{
		return new Result<TValue>(value, true, Error.None);
	}

	public static Result Failure(Error error)
	{
		return new Result(false, error);
	}

	public static Result<TValue> Failure<TValue>(Error error)
	{
		return new Result<TValue>(default, false, error);
	}
}

public sealed class Result<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
	[NotNull]
	public TValue Value => IsSuccess switch
	{
		true => value!,
		false => throw new InvalidOperationException(
			"The value of a failure result can't be accessed.")
	};

	public static implicit operator Result<TValue>(TValue? value)
	{
		if (value is null) return Failure<TValue>(Error.NullValue);

		Result<TValue> success = Success(value);

		return success;
	}

	public Result<TValue> ValidationFailure(Error error)
	{
		return new Result<TValue>(default, false, error);
	}
}