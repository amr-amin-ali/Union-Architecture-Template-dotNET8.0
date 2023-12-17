namespace Elearning.Contracts.Common
{
    public class PagedList<T>
    {
        public PagedList() { }
        public PagedList(IEnumerable<T> source, int pageNumber, int pageSize, int totalcount = 0)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 5;
            }
            if (totalcount == 0)
            {
                TotalItems = totalcount;
                List = source.ToList();
            }
            else
            {
                TotalItems = source.Count();
                List = source
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToList();
            }
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public List<T> List { get; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int NextPageNumber => HasNextPage ? PageNumber + 1 : TotalPages;
        public int PreviousPageNumber => HasPreviousPage ? PageNumber - 1 : 1;

        public PagingHeader GetHeader()
        {
            return new PagingHeader(
                 TotalItems,
                 PageNumber,
                 PageSize,
                 TotalPages);
        }
    }
}
