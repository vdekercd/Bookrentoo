using System.ComponentModel.DataAnnotations;

namespace DamienVDK.Bookrentoo.Common.Requests.Organizations
{
    public class CreateOrganizationRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "The name must be only contains letters, numbers, or symbols _ - and .).")]
        public string Name { get; set; }
    }
}
