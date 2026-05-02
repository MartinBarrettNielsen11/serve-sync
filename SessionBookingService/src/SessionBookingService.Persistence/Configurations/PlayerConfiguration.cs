using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.PlayerAggregate;
using SharedKernel;

namespace SessionBookingService.Persistence.Configurations;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.OwnsOne<Schedule>("_schedule", sb =>
        {
            sb.Property<Dictionary<DateOnly, List<TimeSlot>>>("_calendar");

            sb.Property(s => s.Id);
        });

        builder.Property<List<Guid>>("_sessionIds");

        builder.Property(g => g.UserId);
    }
}
