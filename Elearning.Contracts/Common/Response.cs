namespace Elearning.Contracts.Common
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public Error Error { get; set; } = new Error();
        public bool IsSuccessful => !Error.Errors.Any();

        public Response()
        {
        }
        public Response(Error error)
        {
            Error = error;
        }

        public Response(T value)
        {
            Data = value;
            Error = new Error();
        }
    }
}
