namespace DamienVDK.Bookrentoo.OrganizationApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationService _organizationService;

    public OrganizationController(OrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<ActionResult<Organization>> GetUserOrganizationAsync()
    {
        var userId = this.GetUserId();
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest("Bad token");

        var organization = await _organizationService.GetOrganizationByIdAsync(userId);
        if (organization == null)
        {
            return NotFound();
        }
        return Ok(organization);
    }

    [HttpPost]
    public async Task<ActionResult> AddOrganizationAsync(Organization organization)
    {
        var userId = this.GetUserId();
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest("Bad token");

        await _organizationService.AddOrganizationAsync(userId, organization);
        return Ok();
    }
}
