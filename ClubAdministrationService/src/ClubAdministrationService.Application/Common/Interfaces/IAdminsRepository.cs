using ClubAdministrationService.Domain.AdminAggregate;

namespace ClubAdministrationService.Application.Common.Interfaces;

internal interface IAdminsRepository
{
    Task AddAdminAsync(Admin player);
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
}