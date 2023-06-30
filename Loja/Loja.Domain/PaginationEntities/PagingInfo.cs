namespace Loja.Domain.PaginationEntities
{
    /// <summary>
    /// Objeto para carregar a lista e os dados de paginação
    /// </summary>
    public class PagingInfo
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Cria objeto com os dados de paginação
        /// </summary>
        /// <param name="count">Valor total de itens do banco de dados</param>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        public PagingInfo(int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            // Lógica de arredondamento
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
