using System.Reflection;
using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Infrastructure;

namespace ClubAdministrationService.Tests.Architecture;

public abstract class TestBase
{
	protected static readonly Assembly DomainAssembly = typeof(Club).Assembly;
	protected static readonly Assembly ApplicationAssembly = typeof(IClubsRepository).Assembly;
	protected static readonly Assembly InfrastructureAssembly = typeof(ClubDbContext).Assembly;
	protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
