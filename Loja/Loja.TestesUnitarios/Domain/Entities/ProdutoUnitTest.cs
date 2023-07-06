namespace Loja.TestesUnitarios.Domain.Entities
{
    public class ProdutoUnitTest
    {
        [Fact]
        public void Produto_Match_ValidData()
        {
            // Act
            var produto = new Produto(
                "Produto Teste",
                "Descrição Teste",
                200.00M,
                "produtoTeste.png",
                10,
                DateTime.Parse("12-12-2012"),
                1
                );

            // Assert
            Assert.Equal("Produto Teste", produto.Nome);
            Assert.Equal("Descrição Teste", produto.Descricao);
            Assert.Equal(200M, produto.Preco);
            Assert.Equal("produtoTeste.png", produto.ImagemUrl);
            Assert.Equal(10, produto.Estoque);
            Assert.Equal(DateTime.Parse("12-12-2012"), produto.DataCadastro);
            Assert.Equal(1, produto.CategoriaId);
        }

        [Fact]
        public void Produto_Match_DateTimeNow()
        {
            // Arrange
            var dtNow = DateTime.Now;
            DateTime dt = new DateTime(dtNow.Year, dtNow.Month,
                dtNow.Day, dtNow.Hour, dtNow.Minute, 0, dtNow.Kind);

            // Act
            var produto = new Produto(
                "Produto Teste",
                "Descrição Teste",
                200.00M,
                "produtoTeste.png",
                10,
                null,
                1
                );

            // Assert
            Assert.NotNull(produto.DataCadastro);

            DateTime dtProdutoNow = Assert.IsType<DateTime>(produto.DataCadastro);
            DateTime dtProduto = new DateTime(dtProdutoNow.Year,
                dtProdutoNow.Month, dtProdutoNow.Day, dtProdutoNow.Hour,
                dtProdutoNow.Minute, 0, dtProdutoNow.Kind);

            Assert.Equal(dt, dtProduto);
        }
    }
}
