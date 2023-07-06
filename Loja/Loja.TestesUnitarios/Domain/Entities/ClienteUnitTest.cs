namespace Loja.TestesUnitarios.Domain.Entities
{
    public class ClienteUnitTest
    {
        [Fact]
        public void Cliente_Match_ValidData()
        {
            // Act
            var cliente = new Cliente(
                "Cliente Teste",
                "000-000-000.00",
                DateTime.Parse("10-02-1995"),
                "R. dos testes N.123",
                "email@email.com",
                "(00) 0-0000-0000",
                DateTime.Parse("10-02-2023")
                );

            // Assert
            Assert.Equal("Cliente Teste", cliente.Nome);
            Assert.Equal("000-000-000.00", cliente.Cpf);
            Assert.Equal(DateTime.Parse("10-02-1995"), cliente.Nascimento);
            Assert.Equal("R. dos testes N.123", cliente.Endereco);
            Assert.Equal("email@email.com", cliente.Email);
            Assert.Equal("(00) 0-0000-0000", cliente.Telefone);
            Assert.Equal(DateTime.Parse("10-02-2023"), cliente.DataCadastro);
        }

        [Fact]
        public void Cliente_Match_DateTimeNow()
        {
            // Arrange
            var dtNow = DateTime.Now;
            DateTime dt = new DateTime(dtNow.Year, dtNow.Month,
                dtNow.Day, dtNow.Hour, dtNow.Minute, 0, dtNow.Kind);

            // Act
            var cliente = new Cliente(
                "Cliente Teste",
                "000-000-000.00",
                DateTime.Parse("10-02-1995"),
                "R. dos testes N.123",
                "email@email.com",
                "(00) 0-0000-0000",
                null
                );

            // Assert
            Assert.NotNull(cliente.DataCadastro);

            DateTime dtClienteNow = Assert.IsType<DateTime>(cliente.DataCadastro);
            DateTime dtCliente = new DateTime(dtClienteNow.Year,
                dtClienteNow.Month, dtClienteNow.Day, dtClienteNow.Hour,
                dtClienteNow.Minute, 0, dtClienteNow.Kind);

            Assert.Equal(dt, dtCliente);
        }
    }
}
