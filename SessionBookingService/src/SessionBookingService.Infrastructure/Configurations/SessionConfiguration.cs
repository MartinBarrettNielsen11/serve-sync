using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Infrastructure.Configurations;

internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
	public void Configure(EntityTypeBuilder<Session> builder)
	{
		builder.HasKey(g => g.Id);

		builder.Property(g => g.Id)
				.ValueGeneratedNever();

		builder.Property(s => s.InstructorId);

		builder.OwnsMany<Booking>("_bookings",
								b =>
								{
									b.ToTable("SessionBookings");

									b.HasKey(r => r.Id);

									b.Property(r => r.Id).ValueGeneratedNever();

									b.WithOwner().HasForeignKey("SessionId");

									b.Property(r => r.PlayerId);
								});


		builder.OwnsMany(s => s.Categories,
						cb =>
						{
							cb.ToTable("SessionCategories");

							cb.WithOwner().HasForeignKey("SessionId");
							cb.HasKey("Id");

							cb.Property(c => c.Name);
							cb.Property(c => c.Value);
						})
				.Metadata
				.SetPropertyAccessMode(PropertyAccessMode.Field);


		builder.Property(s => s.MaxPlayerCapacity);

		builder.Property(s => s.Date);

		builder.OwnsOne(s => s.Time);

		builder.Property(s => s.Name);
		builder.Property(s => s.Description);
		builder.Property(s => s.CourtId);
	}
}
