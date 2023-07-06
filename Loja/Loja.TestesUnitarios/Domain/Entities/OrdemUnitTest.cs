using Loja.Domain.Enums;

namespace Loja.TestesUnitarios.Domain.Entities
{
    public class OrdemUnitTest
    {
        [Fact]
        public void Ordem_Match_ValidData()
        {
            // Act
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );

            // Assert
            Assert.Equal(1, ordem.VendedorId);
            Assert.Equal(2, ordem.ClienteId);
            Assert.Equal(EnumStatusVenda.AguardandoPagamento, ordem.StatusVenda);
            Assert.Equal(22.99M, ordem.Total);
            Assert.Equal(DateTime.Parse("10-10-2010"), ordem.DataCriacao);
        }

        [Fact]
        public void Ordem_Match_DateTimeNow()
        {
            // Arrange
            var dtNow = DateTime.Now;
            DateTime dt = new DateTime(dtNow.Year, dtNow.Month,
                dtNow.Day, dtNow.Hour, dtNow.Minute, 0, dtNow.Kind);

            // Act
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                null
                );

            // Assert
            Assert.NotNull(ordem.DataCriacao);

            DateTime dtOrdemNow = Assert.IsType<DateTime>(ordem.DataCriacao);
            DateTime dtOrdem = new DateTime(dtOrdemNow.Year,
                dtOrdemNow.Month, dtOrdemNow.Day, dtOrdemNow.Hour,
                dtOrdemNow.Minute, 0, dtOrdemNow.Kind);

            Assert.Equal(dt, dtOrdem);
        }

        [Fact]
        public void UpdateStatus_AguardandoPagamento_Para_PagamentoAprovado()
        {
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );

            // Act
            ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado);

            // Assert
            Assert.Equal(EnumStatusVenda.PagamentoAprovado, ordem.StatusVenda);
        }

        [Fact]
        public void UpdateStatus_AguardandoPagamento_Para_Cancelado()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );

            // Act
            ordem.UpdateStatus(EnumStatusVenda.Cancelado);

            // Assert
            Assert.Equal(EnumStatusVenda.Cancelado, ordem.StatusVenda);
        }

        [Fact]
        public void UpdateStatus_AguardandoPagamento_Transicao_Invalida()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );

            // Assert
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.AguardandoPagamento));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.EnviadoParaTransportadora));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.Entregue));
        }

        [Fact]
        public void UpdateStatus_PagamentoAprovado_Para_EnviadoParaTransportadora()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );
            ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado);

            // Act
            ordem.UpdateStatus(EnumStatusVenda.EnviadoParaTransportadora);

            // Assert
            Assert.Equal(EnumStatusVenda.EnviadoParaTransportadora, ordem.StatusVenda);
        }

        [Fact]
        public void UpdateStatus_PagamentoAprovado_Para_Cancelado()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );
            ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado);

            // Act
            ordem.UpdateStatus(EnumStatusVenda.Cancelado);

            // Assert
            Assert.Equal(EnumStatusVenda.Cancelado, ordem.StatusVenda);
        }

        [Fact]
        public void UpdateStatus_PagamentoAprovado_Transicao_Invalida()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );

            // Act
            ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado);

            // Assert
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.AguardandoPagamento));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.Entregue));
        }

        [Fact]
        public void UpdateStatus_EnviadoParaTransportadora_Para_Entregue()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );
            ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado);
            ordem.UpdateStatus(EnumStatusVenda.EnviadoParaTransportadora);

            // Act
            ordem.UpdateStatus(EnumStatusVenda.Entregue);

            // Assert
            Assert.Equal(EnumStatusVenda.Entregue, ordem.StatusVenda);
        }

        [Fact]
        public void UpdateStatus_EnviadoParaTransportadora_Transicao_Invalida()
        {
            // Arrange
            var ordem = new Ordem(
                1,
                2,
                22.99M,
                DateTime.Parse("10-10-2010")
                );
            ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado);
            ordem.UpdateStatus(EnumStatusVenda.EnviadoParaTransportadora);

            // Act
            ordem.UpdateStatus(EnumStatusVenda.Entregue);

            // Assert
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.AguardandoPagamento));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.PagamentoAprovado));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.Entregue));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.Cancelado));
            Assert.False(ordem.UpdateStatus(EnumStatusVenda.EnviadoParaTransportadora));
        }
    }
}
