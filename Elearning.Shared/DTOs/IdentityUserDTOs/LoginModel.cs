namespace Elearning.Shared.DTOs.IdentityUserDTOs
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required(ErrorMessage = "Doctor EventName is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
