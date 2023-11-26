using DamienVDK.Bookrentoo.Common.Responses.Organizations;

namespace DamienVDK.Bookrentoo.OrganizationApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class OrganizationController(
    IOrganizationRepository organizationRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OrganizationDashboardResponse>> GetUserOrganizationAsync()
    {
        var userId = this.GetUserId();
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest("Bad token");

        var organization = await organizationRepository.GetByUserIdAsync(userId);
        if (organization == null)
        {
            return NotFound();
        }
        
        var organizationDashboardResponse = mapper.Map<OrganizationDashboardResponse>(organization);
        
        return Ok(organizationDashboardResponse);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreatOrganizationAsync(CreateOrganizationRequest request, CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (string.IsNullOrWhiteSpace(userId)) return BadRequest("Bad token");

        var organization = new Organization()
        {
            Name = request.Name,
            UserId = userId
        };

        await organizationRepository.AddAsync(organization);

        return Ok();
    }
}
