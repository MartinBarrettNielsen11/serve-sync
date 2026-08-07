using System.Reflection;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;
using UserAdministrationService.Infrastructure;

namespace UserAdministrationService.Tests.Architecture;

public abstract class TestBase
{
	protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
	protected static readonly Assembly ApplicationAssembly = typeof(IUsersRepository).Assembly;
	protected static readonly Assembly InfrastructureAssembly = typeof(UserDbContext).Assembly;
	protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
