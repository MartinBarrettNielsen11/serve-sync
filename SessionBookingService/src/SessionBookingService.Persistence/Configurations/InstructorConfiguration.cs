using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.InstructorAggregate;
using SharedKernel;

namespace SessionBookingService.Persistence.Configurations;

internal sealed class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property<List<Guid>>("_sessionIds");

        builder.Property(t => t.UserId);

        builder.OwnsOne<Schedule>("_schedule", sb =>
        {
            sb.Property<Dictionary<DateOnly, List<TimeSlot>>>("_calendar");

            sb.Property(s => s.Id);
        });
    }
}
