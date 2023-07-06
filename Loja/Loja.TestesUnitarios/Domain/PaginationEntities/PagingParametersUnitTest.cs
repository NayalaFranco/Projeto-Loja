namespace Loja.TestesUnitarios.Domain.PaginationEntities
{
    public class PagingParametersUnitTest
    {
        [Fact]
        public void Should_Return_DefaultValues()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string orderedBy = "id";
            string direction = "asc";


            // Act
            var pagingParameters = new PagingParameters();

            // Assert
            Assert.Equal(pageNumber, pagingParameters.PageNumber);
            Assert.Equal(pageSize, pagingParameters.PageSize);
            Assert.Equal(direction, pagingParameters.Direction);
            Assert.Equal(orderedBy, pagingParameters.OrderedBy);
        }

        [Theory]
        [InlineData(51)]
        [InlineData(200)]
        [InlineData(10000)]
        public void PageSize_Should_Return_MaxValue(int pageSize)
        {
            // Arrange
            const int maxPageSize = 50;

            // Act
            var pagingParameters = new PagingParameters()
            {
                PageSize = pageSize
            };

            // Assert
            Assert.Equal(maxPageSize, pagingParameters.PageSize);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Qualquer Coisa")]
        [InlineData("Asc")]
        [InlineData("xxx")]
        public void Direction_Should_Return_asc(string direction)
        {
            // Act
            var pagingParameters = new PagingParameters()
            {
                Direction = direction
            };

            // Assert
            Assert.Equal("asc", pagingParameters.Direction);
        }

        [Theory]
        [InlineData("desc")]
        [InlineData("Desc")]
        public void Direction_Should_Return_desc(string direction)
        {
            // Act
            var pagingParameters = new PagingParameters()
            {
                Direction = direction
            };

            // Assert
            Assert.Equal("desc", pagingParameters.Direction);
        }
    }
}
