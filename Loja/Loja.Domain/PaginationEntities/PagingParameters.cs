namespace Loja.Domain.PaginationEntities
{
    /// <summary>
    /// Objeto com os parâmetros para efetuar a paginação
    /// </summary>
    public class PagingParameters
    {
        // Altere para definir o limite do máximo por pagina
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public string OrderedBy { get; set; } = "id";
    }
}
