using ClubAdministrationService.Domain.AdminAggregate;

namespace ClubAdministrationService.Application.Common.Interfaces;

internal interface IAdminsRepository
{
    Task AddAdminAsync(Admin player, CancellationToken cancellationToken);
    Task<Admin?> GetByIdAsync(Guid adminId, CancellationToken cancellationToken);
    Task UpdateAsync(Admin admin, CancellationToken cancellationToken);
}