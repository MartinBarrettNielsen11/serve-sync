using SharedKernel;

namespace ClubAdministrationService.Domain.ClubAggregate.Events;

internal sealed record CourtRemovedFromClubEvent(Club Club, Guid CourtId) : IDomainEvent;
