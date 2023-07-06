namespace Loja.TestesUnitarios.Domain.Entities
{
    public class CategoriaUnitTest
    {
        [Fact]
        public void Categoria_Match_ValidData()
        {
            // Act
            var categoria = new Categoria("Teste", "Teste categoria", "teste.png");

            // Assert
            Assert.Equal("Teste", categoria.Nome);
            Assert.Equal("Teste categoria", categoria.Descricao);
            Assert.Equal("teste.png", categoria.ImagemUrl);

        }
    }
}
