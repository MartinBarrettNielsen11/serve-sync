namespace ClubAdministrationService.Application.Clubs.CreateClub;

internal sealed record CreateClubCommand(string Name, Guid SubscriptionId);