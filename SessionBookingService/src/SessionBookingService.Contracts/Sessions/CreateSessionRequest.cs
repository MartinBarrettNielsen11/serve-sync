namespace SessionBookingService.Contracts.Sessions;

internal sealed record CreateSessionRequest(
	string Name,
	string Description,
	int MaxPlayerCapacity,
	DateTime StartDateTime,
	DateTime EndDateTime,
	Guid TrainerId,
	List<string> Categories);
