using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Persistence;

internal class SessionDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPublisher _publisher;

    public DbSet<Court> Courts { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Instructor> Instructors { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;

    public SessionDbContext(DbContextOptions options, 
                            IHttpContextAccessor httpContextAccessor,
                            IPublisher publisher) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}