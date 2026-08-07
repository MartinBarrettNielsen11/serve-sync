using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.Common;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Infrastructure.Converters;

namespace SessionBookingService.Infrastructure.Configurations;

internal sealed class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
	public void Configure(EntityTypeBuilder<Instructor> builder)
	{
		builder.HasKey(t => t.Id);

		builder.Property(t => t.Id)
				.ValueGeneratedNever();

		builder.Property<List<Guid>>("_sessionIds")
				.HasListOfIdsConverter()
				.HasColumnName("SessionIds");

		builder.Property(t => t.UserId);

		builder.OwnsOne<Schedule>("_schedule",
								sb =>
								{
									sb.Property<Dictionary<DateOnly, List<TimeSlot>>>("_calendar")
									.HasColumnName("ScheduleCalendar")
									.HasValueJsonConverter();

									sb.Property(s => s.Id)
									.HasColumnName("ScheduleId");
								});
	}
}
