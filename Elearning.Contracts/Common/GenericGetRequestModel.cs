namespace Elearning.Contracts.Common
{
    public class GenericGetRequestModel<T>
    {
        public T Data { get; set; }
        public string Lang { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        

    }
}
