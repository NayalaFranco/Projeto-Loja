namespace Loja.Domain.PaginationEntities
{
    public class PagingList<T>
    {
        public List<T> Items { get; set; }
        public PagingInfo PaginationInfo { get; set; }

        public PagingList(List<T> items, int count, int PageNumber, int PageSize)
        {
            Items = items;
            PaginationInfo = new PagingInfo(count, PageNumber, PageSize);
        }

        public PagingList(List<T> items, PagingInfo paginationInfo)
        {
            Items = items;
            PaginationInfo = paginationInfo;
        }

    }
}
