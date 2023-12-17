namespace Elearning.Shared.DTOs
{
    public class OTPDataDTO
    {
        public long Id { get; set; }
        public string? OTP { get; set; }
        public string? UserId { get; set; }
        public DateTime? OTPExpiration { get; set; }
        public string? Email { get; set; }
    }
}
