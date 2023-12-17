namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessaeForStatusCode(statusCode);
        }

        private string GetDefaultMessaeForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request , You have made",
                401 => "Authorized you are not",
                402 => "Response found it's not",
                403 => "server error occured",
                _ => string.Empty,
            };

        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
