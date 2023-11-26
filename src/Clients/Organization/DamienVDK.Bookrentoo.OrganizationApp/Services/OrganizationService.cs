namespace DamienVDK.Bookrentoo.OrganizationApp.Services;

public class OrganizationService(
        ProtectedLocalStorage storageService, 
        IHttpClientFactory httpClientFactory)
    : IOrganizationService
{

    public async Task<OrganizationDashboardResponse?> GetOrganizationAsync()
    {
        var httpClient = await GetHttpClientWithHeadersAsync();
        var response = await httpClient.GetAsync("Organization");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<OrganizationDashboardResponse>(await response.Content.ReadAsStringAsync());
    }
    
    public async Task CreateOrganizationAsync(CreateOrganizationRequest createOrganizationRequest)
    {
        var httpClient = await GetHttpClientWithHeadersAsync();
        var content = new StringContent(JsonConvert.SerializeObject(createOrganizationRequest), Encoding.UTF8, "application/json");
        await httpClient.PostAsync("Organization", content);
    }
    //
    private async Task<HttpClient> GetHttpClientWithHeadersAsync()
    {
        var firebaseAuth = await storageService.GetAsync<FirebaseAuth>("firebaseAuth");
        var firebaseAuthValue = firebaseAuth.Value;
        var httpClient = httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", firebaseAuthValue!.FirebaseToken);
        return httpClient;
    }
}
