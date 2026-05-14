using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel;

namespace SessionBookingService.Persistence.Configurations;

internal sealed class CourtConfiguration : IEntityTypeConfiguration<Court>
{
    public void Configure(EntityTypeBuilder<Court> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.Property("_maxSessions").HasColumnName("MaxSessions");

        builder.Property<List<Guid>>("_sessionIds").HasColumnName("SessionIds");

        builder.OwnsOne<Schedule>("_schedule", sb =>
        {
            sb.ToJson();

            sb.Property<Dictionary<DateOnly, List<TimeSlot>>>("_calendar").HasColumnName("ScheduleCalendar");

            sb.Property(s => s.Id).HasColumnName("ScheduleId");
        });

        builder.Property(r => r.Name);
        builder.Property(r => r.ClubId);
    }
}

