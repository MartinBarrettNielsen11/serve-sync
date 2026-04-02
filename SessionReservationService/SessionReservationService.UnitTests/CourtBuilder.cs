using Generator;
using SessionReservationService.Domain.CourtAggregate;

namespace SessionReservationService.UnitTests;


[Builder(typeof(Court))]
public sealed partial class CourtBuilder
{
    public static CourtBuilder Minimal() => new CourtBuilder();
    
    public static CourtBuilder Typical() => Minimal();
}