namespace DamienVDK.Bookrentoo.OrganizationApp.Services;

public interface IOrganizationService
{
    Task<OrganizationDashboardResponse?> GetOrganizationAsync();
    Task CreateOrganizationAsync(CreateOrganizationRequest createOrganizationRequest);
}
