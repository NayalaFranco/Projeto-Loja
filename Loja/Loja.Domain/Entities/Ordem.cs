using Loja.Domain.Enums;
using Loja.Domain.Validation;

namespace Loja.Domain.Entities
{
    public sealed class Ordem
    {
        public Ordem(int vendedorId, int clienteId, decimal total, DateTime dataCriacao)
        {
            VendedorId = vendedorId;
            ClienteId = clienteId;
            StatusVenda = EnumStatusVenda.AguardandoPagamento;
            Total = total;
            DataCriacao = dataCriacao;
        }

        public int Id { get; private set; }

        public int VendedorId { get; private set; }
        public Vendedor? Vendedor { get; set; }

        public int ClienteId { get; private set; }
        public Cliente? Cliente { get; set; }

        public IList<OrdemProduto> Produtos { get; set; }

        public decimal Total { get; private set; }

        public EnumStatusVenda StatusVenda { get; private set; }

        public DateTime DataCriacao { get; private set; }

        /// <summary>
        /// Atualiza o status da ordem de venda se for válido.
        /// </summary>
        /// <param name="statusNovo">Novo status a ser inserido</param>
        public void UpdateStatus(EnumStatusVenda statusNovo)
        {
            //De: "Aguardando pagamento" para: "Pagamento Aprovado" ou "Cancelado"
            if (StatusVenda == EnumStatusVenda.AguardandoPagamento
                && (statusNovo == EnumStatusVenda.PagamentoAprovado
                || statusNovo == EnumStatusVenda.Cancelado))
            {
                StatusVenda = statusNovo;
            }
            //De: "Pagamento Aprovado" para: "Enviado para Transportadora" ou "Cancelado"
            else if (StatusVenda == EnumStatusVenda.PagamentoAprovado &&
                (statusNovo == EnumStatusVenda.EnviadoParaTransportadora
                || statusNovo == EnumStatusVenda.Cancelado))
            {
                StatusVenda = statusNovo;
            }
            //De: "Enviado para Transportador" para: "Entregue"
            else if (StatusVenda == EnumStatusVenda.EnviadoParaTransportadora
                && statusNovo == EnumStatusVenda.Entregue)
            {
                StatusVenda = statusNovo;
            }
            else
            {
                DomainExceptionValidation.When(true,
                    "Transição de Status Inválida");
            }
        }

    }
}
