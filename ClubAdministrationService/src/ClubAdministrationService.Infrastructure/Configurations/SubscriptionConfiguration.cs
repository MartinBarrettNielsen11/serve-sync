using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubAdministrationService.Infrastructure.Configurations;

internal sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
	public void Configure(EntityTypeBuilder<Subscription> builder)
	{
		builder.HasKey(s => s.Id);

		builder.Property(s => s.Id)
			.ValueGeneratedNever();

		builder.Property("_maxCourtsAllowed")
			.HasColumnName("MaxCourtsAllowed");

		builder.Property(s => s.SubscriptionType)
			.HasConversion(subscriptionType => subscriptionType.Value,
				value => SubscriptionType.FromValue(value));

		builder.Property<List<Guid>>("_clubIds")
			.HasColumnName("ClubIds")
			.HasListOfIdsConverter();
	}
}