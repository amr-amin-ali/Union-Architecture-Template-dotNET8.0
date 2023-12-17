namespace Elearning.Contracts.Common
{
    public class ListResponse<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
