namespace DamienVDK.Bookrentoo.Common.Responses.Organizations;

public class OrganizationDashboardResponse
{
    public string Name { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public List<Member> Members { get; set; } = new List<Member>();
}

public class Member
{
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}