namespace DamienVDK.Bookrentoo.OrganizationApi.Services;

public class OrganizationRepository(OrganizationDbContext context) : IOrganizationRepository
{
    public async Task<Organization> AddAsync(Organization organization)
    {
        context.Organizations.Add(organization);
        await context.SaveChangesAsync();
        return organization;
    }

    public async Task<Organization?> GetByUserIdAsync(string userId)
    {
        return await context.Organizations.FirstOrDefaultAsync(o => o.UserId.Equals(userId));
    }
}
