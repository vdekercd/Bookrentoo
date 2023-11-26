
namespace DamienVDK.Bookrentoo.OrganizationApi.Services
{
    public interface IOrganizationRepository
    {
        Task<Organization> AddAsync(Organization organization);
        Task<Organization?> GetByUserIdAsync(string userId);
    }
}