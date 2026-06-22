using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Infrastructure.Converters;
using SharedKernel;

namespace SessionBookingService.Infrastructure.Configurations;

internal sealed class CourtConfiguration : IEntityTypeConfiguration<Court>
{
    public void Configure(EntityTypeBuilder<Court> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.Property("_maxDailySessions").HasColumnName("MaxDailySessions");

        builder.Property<List<Guid>>("_sessionIds").HasColumnName("SessionIds").HasListOfIdsConverter();

        builder.OwnsOne<Schedule>("_schedule", sb =>
        {
            sb.Property<Dictionary<DateOnly, List<TimeSlot>>>("_calendar")
                .HasColumnName("ScheduleCalendar")
                .HasValueJsonConverter();
            
            sb.Property(s => s.Id).HasColumnName("ScheduleId");
        });

        builder.Property(r => r.Name);
        builder.Property(r => r.ClubId);
    }
}

