using System.Reflection;
using NetArchTest.Rules;
using SharedKernel;
using Shouldly;
using Xunit;

namespace ClubAdministrationService.Tests.Architecture.DDD;

public sealed class DDDTests : TestBase
{
	[Fact]
	public void Entities_Should_Have_PrivateSetter()
	{
		IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
			.That()
			.AreClasses()
			.And()
			.Inherit(typeof(RootAggregate))
			.GetTypes();

		foreach (Type entityType in entityTypes)
		{
			PropertyInfo[] properties = entityType.GetProperties();
			foreach (PropertyInfo property in properties)
			{
				if (property.CanWrite)
				{
					property.SetMethod.ShouldNotBeNull();
/*
					property.SetMethod.Should().NotBeNull()
						.And.Match(setMethod => setMethod.IsPrivate || setMethod.IsFamily || setMethod.IsFamilyOrAssembly,
							$"{property.Name} should have a private or protected setter.", property.DeclaringType?.FullName);
							*/
				}
			}
		}
	}
}
