namespace Loja.TestesUnitarios.Domain.PaginationEntities
{
    public class PagingInfoUnitTest
    {
        [Fact]
        public void PagingInfo_Match_ValidData()
        {
            // Arrange
            int totalDeItens = 20;
            int numeroDaPagina = 1;
            int itensPorPagina = 5;
            int totalDePaginas = 4;
            bool paginaAnterior = false;
            bool proximaPagina = true;


            // Act
            var pagingInfo = new PagingInfo(totalDeItens, numeroDaPagina, itensPorPagina);

            // Assert
            Assert.Equal(totalDeItens, pagingInfo.TotalCount);
            Assert.Equal(itensPorPagina, pagingInfo.PageSize);
            Assert.Equal(numeroDaPagina, pagingInfo.CurrentPage);
            Assert.Equal(totalDePaginas, pagingInfo.TotalPages);
            Assert.Equal(paginaAnterior, pagingInfo.HasPrevious);
            Assert.Equal(proximaPagina, pagingInfo.HasNext);
        }

        [Fact]
        public void PagingInfo_Arredondamento_TotalPages()
        {
            // Arrange
            int totalDeItens = 20;
            int numeroDaPagina = 1;
            int itensPorPagina = 3;

            int totalDePaginas = 7;


            // Act
            var pagingInfo = new PagingInfo(totalDeItens, numeroDaPagina, itensPorPagina);

            // Assert
            Assert.Equal(totalDePaginas, pagingInfo.TotalPages);
        }
    }
}
