using Microsoft.Extensions.Logging;

namespace ClubAdministrationService.Application.Clubs.Commands.CreateClub;

internal static partial class CreateClubLogging
{
	[LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "Club created: {ClubName}")]
	public static partial void ClubCreated(this ILogger logger, string clubName);
}
