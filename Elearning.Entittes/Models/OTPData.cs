using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Entittes.Models
{
    public class OTPData
    {
        public long Id { get; set; } 
        public string? OTP { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? UserId  { get; set; } 
        public ApplicationUser? AppUser { get; set; }
        public DateTime? OTPExpiration { get; set; }    
        public string? Email { get; set; }  
    }
}
