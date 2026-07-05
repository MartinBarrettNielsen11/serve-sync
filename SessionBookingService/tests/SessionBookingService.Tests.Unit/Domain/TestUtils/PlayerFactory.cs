using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;

namespace SessionBookingService.Tests.Unit.Domain.TestUtils;

internal static class PlayerFactory
{
	internal static Player Create(
		Guid? userId = null,
		Guid? id = null)
	{
		return new Player(
			userId ?? UserConstants.Id,
			id: id ?? PlayerConstants.Id);
	}
}