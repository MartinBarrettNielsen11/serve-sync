using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAdministrationService.Infrastructure.IntegrationEvents;

namespace UserAdministrationService.Infrastructure.Configurations;

internal sealed class OutboxIntegrationEventConfiguration : IEntityTypeConfiguration<OutboxIntegrationEvent>
{
	public void Configure(EntityTypeBuilder<OutboxIntegrationEvent> builder)
	{
		builder.Property<int>("Id").ValueGeneratedOnAdd();

		builder.HasKey("Id");

		builder.Property(o => o.EventName);
		builder.Property(o => o.EventContent);
	}
}
