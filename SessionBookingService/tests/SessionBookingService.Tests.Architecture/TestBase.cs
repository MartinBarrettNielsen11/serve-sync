using System.Reflection;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Infrastructure;

namespace SessionBookingService.Tests.Architecture;

public abstract class TestBase
{
    protected static readonly Assembly DomainAssembly = typeof(Session).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ISessionsRepository).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(SessionBookingDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
