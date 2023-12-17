namespace Elearning.Contracts.Common
{
    public class GenericResponseModel<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
    }
}
