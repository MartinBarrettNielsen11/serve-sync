using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Utils;

public static class SessionCategoryUtils
{
	public static Result<List<SessionCategory>> ToDomain(ICollection<string>? categories)
	{
		if (categories is null)
		{
			return new List<SessionCategory>();
		}

		List<SessionCategory> parsedCategories = categories
			.Select(category => SessionCategory.TryFromName(category, out SessionCategory? parsedCategory)
				? parsedCategory
				: null)
			.Where(category => category is not null)
			.ToList()!;

		if (parsedCategories.Count != categories.Count)
		{
			List<Error> res = categories.Except(parsedCategories.ConvertAll(c => c.Name), StringComparer.Ordinal)
				.Select(invalidCategory =>
					Error.Problem("Categories.InvalidCategory", $"Invalid category '{invalidCategory}'"))
				.ToList();

			return Result.Failure<List<SessionCategory>>(new ValidationError(res.ToArray()));
		}

		return parsedCategories;
	}
}
