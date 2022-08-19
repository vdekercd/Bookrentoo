namespace DamienVDK.Bookrentoo.OrganizationApi.Policies;

public class TransientHttpFailureRetryPolicy
{
    private const int MaxRetryCount = 5;

    private const double DurationBetweenRetries = 250;

    private readonly AsyncRetryPolicy<HttpResponseMessage> policy;

    public TransientHttpFailureRetryPolicy()
    {
        this.policy = Policy
            .Handle<HttpRequestException>()
            .Or<TaskCanceledException>()
            .OrResult<HttpResponseMessage>(response =>
                response.StatusCode == HttpStatusCode.RequestTimeout ||
                response.StatusCode == HttpStatusCode.BadGateway ||
                response.StatusCode == HttpStatusCode.GatewayTimeout ||
                response.StatusCode == HttpStatusCode.ServiceUnavailable
            )
            .WaitAndRetryAsync(
                MaxRetryCount,
                retryCount => TimeSpan.FromMilliseconds(DurationBetweenRetries * Math.Pow(2, retryCount - 1))
            );
    }

    public async Task<HttpResponseMessage> ExecuteAsync(Func<Task<HttpResponseMessage>> action)
    {
        return await policy.ExecuteAsync(action);
    }
}
