namespace SessionBookingService.Contracts.Sessions;

internal sealed record SessionResponse(Guid Id,
										string Name,
										string Description,
										int NumPlayers,
										int MaxPlayerCapacity,
										DateTime StartDateTime,
										DateTime EndDateTime,
										List<string> Categories);
