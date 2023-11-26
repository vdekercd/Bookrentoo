namespace DamienVDK.Bookrentoo.OrganizationApi.Entities;

public class Member
{
    public long Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int OrganizationId { get; set; }
}
