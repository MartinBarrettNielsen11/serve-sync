namespace SharedKernel.Results;

public sealed class EmptyResult
{
	public static readonly EmptyResult Default = new();

	private EmptyResult()
	{
	}
}
