namespace Elearning.Shared.DTOs.IdentityUserDTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRole
    {
        [Required]
        public string RoleName { get; set; }
    }
}
