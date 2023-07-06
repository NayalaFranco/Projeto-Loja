namespace Loja.TestesUnitarios.Domain.PaginationEntities
{
    public class PagingListUnitTest
    {
        [Fact]
        public void PaginationList_Match_ValidData()
        {
            // Arrange
            var items = new List<Categoria>()
            {
                new Categoria("teste", "unitario", "img"),
                new Categoria("teste2", "unitario2", "img2"),
            };

            int count = 10;
            int pageNumber = 2;
            int pageSize = 3;

            // Act
            var pagingList = new PagingList<Categoria>(items, count, pageNumber, pageSize);

            // Assert
            Assert.Equal(items, pagingList.Items);
            Assert.Equal(count, pagingList.PaginationInfo.TotalCount);
            Assert.Equal(pageNumber, pagingList.PaginationInfo.CurrentPage);
            Assert.Equal(pageSize, pagingList.PaginationInfo.PageSize);
        }
    }
}
