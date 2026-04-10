using SessionReservationService.UnitTests.Domain.TestUtils;
using Xunit;

namespace SessionReservationService.UnitTests.Domain.SessionAggregate;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreCourts_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant = ParticipantFactory.CreateParticipant();

}