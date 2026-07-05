using ClubAdministrationService.Domain.CourtAggregate;
using SharedKernel;

namespace ClubAdministrationService.Domain.ClubAggregate.Events;

#pragma warning disable MSG0005
internal sealed record CourtAddedToClubEvent(Club Club, Court Court) : IDomainEvent;
#pragma warning restore MSG0005