namespace DamienVDK.Bookrentoo.OrganizationApi.Extensions;

public static class ControllerBaseExtentions
{
    public static string GetUserId(this ControllerBase @this)
    {
        return @this.User?.Claims?
            .FirstOrDefault(item => item.Type == "user_id")?.Value ?? string.Empty;
    }
}