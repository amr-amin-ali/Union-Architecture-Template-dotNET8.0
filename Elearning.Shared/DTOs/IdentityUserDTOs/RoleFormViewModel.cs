using System.ComponentModel.DataAnnotations;

namespace Elearning.Shared.DTOs.IdentityUserDTOs
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}