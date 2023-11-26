namespace DamienVDK.Bookrentoo.OrganizationApp.Services;

public class OrganizationService(
        ProtectedLocalStorage storageService,
        HttpClient httpClient)
    : IOrganizationService
{

    public async Task<OrganizationDashboardResponse?> GetOrganizationAsync()
    {
        var currentHttpClient = await GetHttpClientWithHeadersAsync();
        var response = await currentHttpClient.GetAsync("Organization");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<OrganizationDashboardResponse>(await response.Content.ReadAsStringAsync());
    }
    
    public async Task CreateOrganizationAsync(CreateOrganizationRequest createOrganizationRequest)
    {
        var currentHttpClient = await GetHttpClientWithHeadersAsync();
        var content = new StringContent(JsonConvert.SerializeObject(createOrganizationRequest), Encoding.UTF8, "application/json");
        var response = await currentHttpClient.PostAsync("Organization", content);
        response.EnsureSuccessStatusCode();
    }
    //
    private async Task<HttpClient> GetHttpClientWithHeadersAsync()
    {
        var firebaseAuth = await storageService.GetAsync<FirebaseAuth>("firebaseAuth");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", firebaseAuth.Value!.FirebaseToken);
        return httpClient;
    }
}
