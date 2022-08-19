namespace DamienVDK.Bookrentoo.OrganizationApp.Extensions;

public static class AuthenticationStateExtensions
{
    public static string GetUsername(this AuthenticationState authenticationState)
    {
        if (!authenticationState?
            .User?
            .Claims?
            .Any(item => item.Type.Equals("name", StringComparison.OrdinalIgnoreCase)) ?? true) 
            return string.Empty;

        return authenticationState!.User.Claims.First(item => item.Type.Equals("name", StringComparison.OrdinalIgnoreCase)).Value;
    }

    public static bool IsConnected(this AuthenticationState authenticationState)
    {
        return authenticationState?.User?.Claims?.Any() ?? false;
    }
}

