namespace DamienVDK.Bookrentoo.OrganizationApi.Services;

internal class FirebaseSigningKeysGenerator
{
    private static HttpClient _httpClient = new HttpClient();

    internal async Task<IEnumerable<SecurityKey>> GetKeysAsync()
    {
        var retryPolicy = new TransientHttpFailureRetryPolicy();

        using (var response = await retryPolicy.ExecuteAsync(GetTokensAsync))
        {
            response.EnsureSuccessStatusCode();
            var x509Data = await response.Content.ReadAsAsync<Dictionary<string, string>>();
            return x509Data.Values
                .Select(data => new X509SecurityKey(new X509Certificate2(Encoding.UTF8.GetBytes(data))))
                .ToArray();
        }
    }

    private static Task<HttpResponseMessage> GetTokensAsync()
    {
        return _httpClient.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://www.googleapis.com/robot/v1/metadata/x509/securetoken@system.gserviceaccount.com")
        });
    }
}
