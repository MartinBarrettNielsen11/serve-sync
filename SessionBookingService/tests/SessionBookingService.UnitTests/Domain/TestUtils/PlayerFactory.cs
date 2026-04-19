using System;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.UnitTests.Domain.Constants;

namespace SessionBookingService.UnitTests.Domain.TestUtils;

internal static class PlayerFactory
{
    internal static Player Create(
        Guid? userId = null,
        Guid? id = null)
    {
        return new Player(
            userId: userId ?? UserConstants.Id,
            id: id ?? PlayerConstants.Id);
    }
}