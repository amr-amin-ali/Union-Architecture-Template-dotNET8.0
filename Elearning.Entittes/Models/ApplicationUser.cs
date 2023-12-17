
namespace Elearning.Entittes.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {

        public byte[]? Image { get; set; }
    }
}
