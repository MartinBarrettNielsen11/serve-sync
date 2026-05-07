using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministrationService.Persistence.Repositories;

internal sealed class AdminsRepository(ClubDbContext clubDbContext) : IAdminsRepository
{
    public async Task AddAdminAsync(Admin player)
    {
        await clubDbContext.Admins.AddAsync(player);
        await clubDbContext.SaveChangesAsync();
    }

    public Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return clubDbContext.Admins.FirstOrDefaultAsync(a => a.Id == adminId);
    }

    public async Task UpdateAsync(Admin admin)
    {
        clubDbContext.Update(admin);
        await clubDbContext.SaveChangesAsync();
    }
}

