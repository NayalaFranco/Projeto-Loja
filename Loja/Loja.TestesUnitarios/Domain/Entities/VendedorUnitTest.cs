namespace Loja.TestesUnitarios.Domain.Entities
{
    public class VendedorUnitTest
    {
        [Fact]
        public void Vendedor_Match_ValidData()
        {
            // Act
            var vendedor = new Vendedor(
                "Vendedor Teste",
                "000-000-000.00",
                DateTime.Parse("22-04-1980"),
                "R. dos Vendedores N.456",
                "email@email.com",
                "(00) 0-0000-0000",
                DateTime.Parse("11-11-2011")
                );

            // Assert
            Assert.Equal("Vendedor Teste", vendedor.Nome);
            Assert.Equal("000-000-000.00", vendedor.Cpf);
            Assert.Equal(DateTime.Parse("22-04-1980"), vendedor.Nascimento);
            Assert.Equal("R. dos Vendedores N.456", vendedor.Endereco);
            Assert.Equal("email@email.com", vendedor.Email);
            Assert.Equal("(00) 0-0000-0000", vendedor.Telefone);
            Assert.Equal(DateTime.Parse("11-11-2011"), vendedor.DataCadastro);
        }

        [Fact]
        public void Vendedor_Match_DateTimeNow()
        {
            // Arrange
            var dtNow = DateTime.Now;
            DateTime dt = new DateTime(dtNow.Year, dtNow.Month,
                dtNow.Day, dtNow.Hour, dtNow.Minute, 0, dtNow.Kind);

            // Act
            var vendedor = new Vendedor(
                "Vendedor Teste",
                "000-000-000.00",
                DateTime.Parse("22-04-1980"),
                "R. dos Vendedores N.456",
                "email@email.com",
                "(00) 0-0000-0000",
                null
                );

            // Assert
            Assert.NotNull(vendedor.DataCadastro);

            DateTime dtVendedorNow = Assert.IsType<DateTime>(vendedor.DataCadastro);
            DateTime dtVendedor = new DateTime(dtVendedorNow.Year,
                dtVendedorNow.Month, dtVendedorNow.Day, dtVendedorNow.Hour,
                dtVendedorNow.Minute, 0, dtVendedorNow.Kind);

            Assert.Equal(dt, dtVendedor);
        }
    }
}
