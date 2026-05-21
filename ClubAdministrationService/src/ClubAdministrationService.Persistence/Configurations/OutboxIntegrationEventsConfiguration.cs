using ClubAdministrationService.Persistence.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubAdministrationService.Persistence.Configurations;

public class OutboxIntegrationEventsConfiguration : IEntityTypeConfiguration<OutboxIntegrationEvent>
{
    public void Configure(EntityTypeBuilder<OutboxIntegrationEvent> builder)
    {
        builder.Property<int>("Id")
            .ValueGeneratedOnAdd();

        builder.HasKey("Id");

        builder.Property(o => o.EventName);

        builder.Property(o => o.EventContent);
    }
}