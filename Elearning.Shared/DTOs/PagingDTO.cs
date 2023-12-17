namespace Elearning.Shared.DTOs
{
    public class PagingDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Take => PageSize;
        public int Skip => Take * ( PageNumber - 1 );

    }
}