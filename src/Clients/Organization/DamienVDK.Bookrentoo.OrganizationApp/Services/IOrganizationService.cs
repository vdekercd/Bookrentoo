namespace DamienVDK.Bookrentoo.OrganizationApp.Services
{
    public interface IOrganizationService
    {
        Task<Organization?> GetOrganizationAsync();
        Task CreateOrganizationAsync(Organization organization);
    }
}