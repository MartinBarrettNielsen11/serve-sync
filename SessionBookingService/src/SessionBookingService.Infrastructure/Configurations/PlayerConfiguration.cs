using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Infrastructure.Converters;
using SharedKernel;

namespace SessionBookingService.Infrastructure.Configurations;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
	public void Configure(EntityTypeBuilder<Player> builder)
	{
		builder.HasKey(g => g.Id);

		builder.Property(g => g.Id)
			.ValueGeneratedNever();

		builder.OwnsOne<Schedule>("_schedule", sb =>
		{
			sb.Property<Dictionary<DateOnly, List<TimeSlot>>>("_calendar")
				.HasColumnName("ScheduleCalender")
				.HasValueJsonConverter();

			sb.Property(s => s.Id).HasColumnName("ScheduleId");
		});

		builder.Property<List<Guid>>("_sessionIds")
			.HasColumnName("SessionIds")
			.HasListOfIdsConverter();

		builder.Property(g => g.UserId);
	}
}