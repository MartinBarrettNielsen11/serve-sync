using ClubAdministrationService.Domain.ClubAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClubAdministrationService.Persistence.Configurations;

internal sealed class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property("_maxCourtCapacity")
            .HasColumnName("MaxCourtCapacity");

        builder.Property<List<Guid>>("_courtIds")
            .HasColumnName("CourtIds");

        builder.Property<List<Guid>>("_instructorIds")
            .HasColumnName("InstructorIds");

        builder.Property(g => g.Name);

        builder.Property(g => g.SubscriptionId);
    }
}
