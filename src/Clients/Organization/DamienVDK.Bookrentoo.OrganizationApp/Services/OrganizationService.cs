using Newtonsoft.Json;
using System.Text;

namespace DamienVDK.Bookrentoo.OrganizationApp.Services;

public class OrganizationService : IOrganizationService
{
    private readonly ProtectedLocalStorage _storageService;

    public OrganizationService(ProtectedLocalStorage storageService)
    {
        _storageService = storageService;
    }

    public async Task<Organization?> GetOrganizationAsync()
    {
        var client = new DaprClientBuilder().Build();
        var request = await GetRequestMessageWithHeadersAsync(client, HttpMethod.Get, "Organization");
        var response =  await client.InvokeMethodWithResponseAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<Organization>(await response.Content.ReadAsStringAsync());
    }

    public async Task CreateOrganizationAsync(Organization organization)
    {
        var client = new DaprClientBuilder().Build();
        var request = await GetRequestMessageWithHeadersAsync(client, HttpMethod.Post, "Organization");
        request.Content = new StringContent(JsonConvert.SerializeObject(organization), Encoding.UTF8, "application/json");
        await client.InvokeMethodAsync(request);
    }

    private async Task<HttpRequestMessage> GetRequestMessageWithHeadersAsync(DaprClient client, HttpMethod method, string methodName)
    {
        var firebaseAuth = await _storageService.GetAsync<FirebaseAuth>("firebaseAuth");
        var firebaseAuthValue = firebaseAuth.Value;
        var request = client.CreateInvokeMethodRequest(method, Components.ORGANIZATION_API, methodName);
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", firebaseAuthValue!.FirebaseToken);
        return request;
    }
}
