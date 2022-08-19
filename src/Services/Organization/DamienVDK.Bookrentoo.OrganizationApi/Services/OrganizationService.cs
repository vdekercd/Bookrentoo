using Dapr.Client;

namespace DamienVDK.Bookrentoo.OrganizationApi.Services;

public class OrganizationService
{
    public async Task<Organization> GetOrganizationByIdAsync(string userId)
    {

        var daprClient = new DaprClientBuilder().Build();
        var organization = await daprClient.GetStateAsync<Organization>("bookrentoodb", userId);
        return organization;
    }

    public async Task AddOrganizationAsync(string userId, Organization organization)
    {
        var daprClient = new DaprClientBuilder().Build();
        await daprClient.SaveStateAsync("bookrentoodb", userId, organization);
    }
}
