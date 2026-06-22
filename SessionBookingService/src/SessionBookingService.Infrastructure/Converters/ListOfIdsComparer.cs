using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SessionBookingService.Infrastructure.Converters;

public class ListOfIdsComparer : ValueComparer<List<Guid>>
{
    public ListOfIdsComparer() : base(
        (t1, t2) => t1!.SequenceEqual(t2!),
        t => t.Select(x => x!.GetHashCode()).Aggregate((x, y) => x ^ y),
        t => t)
    {
    }
}