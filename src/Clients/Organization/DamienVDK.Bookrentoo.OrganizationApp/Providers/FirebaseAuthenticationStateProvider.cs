using Firebase.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace DamienVDK.Bookrentoo.OrganizationApp.Providers;

public class FirebaseAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _storageService;
    private readonly FirebaseAuthProvider _firebaseAuthProvider;
    private const string APIKey = "-api-KEY";

    public FirebaseAuthenticationStateProvider(ProtectedLocalStorage storageService)
    {
        _storageService = storageService;
        var config = new FirebaseConfig(APIKey);
        _firebaseAuthProvider = new FirebaseAuthProvider(config);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var firebaseAuthValue = await _storageService.GetAsync<FirebaseAuth>("firebaseAuth");
            var firebaseAuth = firebaseAuthValue.Value;

            if (firebaseAuth is null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(await ParseClaimsFromJwtAsync(firebaseAuth), "jwt")));
        }
        catch
        {
            // Will trhown while prerender
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }

    public void NotifyAuthenticationStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private async Task<IEnumerable<Claim>> ParseClaimsFromJwtAsync(FirebaseAuth? auth)
    {
        var claims = new List<Claim>();
        var token = new JwtSecurityToken(auth.FirebaseToken);

        if (token.ValidTo <= DateTime.Now.AddMinutes(10))
        {
            var newauth = await _firebaseAuthProvider.RefreshAuthAsync(auth);
            await _storageService.SetAsync("firebaseAuth", newauth);
        }

        return token.Claims;
    }
}