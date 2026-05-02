using ClubAdministrationService.Domain.CourtAggregate;
using SharedKernel;

namespace ClubAdministrationService.Domain.ClubAggregate.Events;

internal sealed record CourtAddedEvent(Club Club, Court court) : IDomainEvent;