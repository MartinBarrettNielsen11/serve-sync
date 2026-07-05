using SharedKernel;

namespace ClubAdministrationService.Domain.ClubAggregate.Events;

#pragma warning disable MSG0005
internal sealed record CourtRemovedFromClubEvent(Club Club, Guid CourtId) : IDomainEvent;
#pragma warning restore MSG0005