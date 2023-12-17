namespace Elearning.Shared.DTOs.IdentityUserDTOs
{
    public class RequestResponse<T>
    {

        public string? Message { get; set; }
        public T? Data { get; set; }

        public int? StatusCode { get; set; }
    }
    public enum RequestStatus
    {
        Success,
        Fail
    }
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "Doctor";
    }
}
