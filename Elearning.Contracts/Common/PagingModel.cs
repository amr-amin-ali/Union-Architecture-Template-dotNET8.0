namespace Elearning.Contracts.Common
{
    public class PagingModel<T>
    {
        public PagingHeader Paging { get; set; }
        public List<LinkInfo> Links { get; set; }
        public List<T> Data { get; set; }
    }
}
