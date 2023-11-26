
using DamienVDK.Bookrentoo.OrganizationApp.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace DamienVDK.Bookrentoo.OrganizationApp.Services;

public class AuthFirebaseService : IAuthFirebaseService
{
    private const string APIKey = "_API_KEY"; // TODO Setttings
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly FirebaseAuthProvider _firebaseAuthProvider;

    public AuthFirebaseService(ProtectedLocalStorage localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;

        var config = new FirebaseConfig(APIKey);
        _firebaseAuthProvider = new FirebaseAuthProvider(config);
    }

    public async Task Login(string email, string password)
    {
        var result = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
        await _localStorage.SetAsync("firebaseAuth", result);
        ((FirebaseAuthenticationStateProvider)_authenticationStateProvider).NotifyAuthenticationStateChanged();
    }

    public async Task Register(string email, string password, string displayName)
    {
        await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, password, displayName);
    }

    public async Task Logout()
    {
        await _localStorage.DeleteAsync("firebaseAuth");
        ((FirebaseAuthenticationStateProvider)_authenticationStateProvider).NotifyAuthenticationStateChanged();
    }

    public async Task ResetPassword(string email)
    {
        await _firebaseAuthProvider.SendPasswordResetEmailAsync(email);
    }
}