namespace Loja.TestesUnitarios.Domain.Entities
{
    public class OrdemProdutoUnitTest
    {
        [Fact]
        public void OrdemProduto_Match_ValidData()
        {
            // Act
            var ordemProduto = new OrdemProduto(
                1,
                2,
                "Produto Teste",
                19.99M,
                1,
                9.99M
                );

            // Assert
            Assert.Equal(1, ordemProduto.OrdemId);
            Assert.Equal(2, ordemProduto.ProdutoId);
            Assert.Equal("Produto Teste", ordemProduto.NomeProduto);
            Assert.Equal(19.99M, ordemProduto.PrecoUnitario);
            Assert.Equal(1, ordemProduto.Quantidade);
            Assert.Equal(9.99M, ordemProduto.Desconto);
        }
    }
}
