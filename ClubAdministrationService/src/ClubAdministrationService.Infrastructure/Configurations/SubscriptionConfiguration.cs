using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubAdministrationService.Infrastructure.Configurations;

internal sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(keyExpression: s => s.Id);

        builder.Property(propertyExpression: s => s.Id)
            .ValueGeneratedNever();

        builder.Property(propertyName: "_maxCourtsAllowed")
            .HasColumnName("MaxCourtsAllowed");

        builder.Property(propertyExpression: s => s.SubscriptionType)
            .HasConversion(convertToProviderExpression: subscriptionType => subscriptionType.Value,
                           convertFromProviderExpression: value => SubscriptionType.FromValue(value));

        builder.Property<List<Guid>>(propertyName: "_clubIds")
            .HasColumnName(name: "ClubIds")
            .HasListOfIdsConverter();
    }
}