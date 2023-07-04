namespace Loja.Domain.PaginationEntities
{
    public class PagingList<T>
    {
        public List<T> Items { get; private set; }
        public PagingInfo PaginationInfo { get; private set; }

        public PagingList(List<T> items, int count, int PageNumber, int PageSize)
        {
            Items = items;
            PaginationInfo = new PagingInfo(count, PageNumber, PageSize);
        }
    }
}
