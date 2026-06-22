using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministrationService.Infrastructure.Repositories;

internal sealed class AdminsRepository(ClubDbContext clubDbContext) : IAdminsRepository
{
    public async Task AddAdminAsync(Admin player, CancellationToken cancellationToken)
    {
        await clubDbContext.Admins.AddAsync(player, cancellationToken);
        await clubDbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Admin?> GetByIdAsync(Guid adminId, CancellationToken cancellationToken)
    {
        return clubDbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId, cancellationToken);
    }

    public async Task UpdateAsync(Admin admin, CancellationToken cancellationToken)
    {
        clubDbContext.Update(admin);
        await clubDbContext.SaveChangesAsync(cancellationToken);
    }
}

