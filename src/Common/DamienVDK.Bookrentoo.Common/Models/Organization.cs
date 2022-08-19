using MongoDB.Bson;

namespace DamienVDK.Bookrentoo.Common.Models;

public class Organization
{
    public ObjectId Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Member> Members { get; set; } = new List<Member>();
}
