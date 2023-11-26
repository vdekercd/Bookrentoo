namespace DamienVDK.Bookrentoo.OrganizationApi.Entities;

public class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public List<Member> Members { get; set; } = new List<Member>();
}
