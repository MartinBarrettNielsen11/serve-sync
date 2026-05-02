using SharedKernel;

namespace ClubAdministrationService.Domain.ClubAggregate.Events;

internal sealed record CourtRemovedEvent(Club club, Guid CourtId) : IDomainEvent;