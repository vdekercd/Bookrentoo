namespace DamienVDK.Bookrentoo.OrganizationApp.Services;

public interface IAuthFirebaseService
{
    Task Login(string email, string password);
    Task Register(string email, string password, string displayName);
    Task Logout();
    Task ResetPassword(string email);
}