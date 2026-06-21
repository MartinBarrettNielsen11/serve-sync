using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Persistence.Configurations;

internal class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName);
        builder.Property(u => u.LastName);
        builder.Property(u => u.Email);
        builder.Property(u => u.AdminId);
        builder.Property(u => u.PlayerId);
        builder.Property(u => u.InstructorId);
        builder.Property("_passwordHash").HasColumnName("PasswordHash");
    }
}