using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClubAdministrationService.Infrastructure.Converters;

public sealed class ListOfIdsComparer() : ValueComparer<List<Guid>>((t1, t2) => t1!.SequenceEqual(t2!),
	t => t.Select(x => x!.GetHashCode()).Aggregate((x, y) => x ^ y),
	t => t);
