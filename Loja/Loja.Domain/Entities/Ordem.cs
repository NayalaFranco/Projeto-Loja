using Loja.Domain.Enums;

namespace Loja.Domain.Entities
{
    public sealed class Ordem
    {
        public Ordem(int vendedorId, int clienteId, decimal total, DateTime? dataCriacao)
        {
            VendedorId = vendedorId;
            ClienteId = clienteId;
            StatusVenda = EnumStatusVenda.AguardandoPagamento;
            Total = total;
            DataCriacao = (dataCriacao == null) ? DateTime.Now : dataCriacao;
        }

        public int Id { get; private set; }

        public int VendedorId { get; private set; }
        public Vendedor? Vendedor { get; set; }

        public int ClienteId { get; private set; }
        public Cliente? Cliente { get; set; }

        public IList<OrdemProduto> Produtos { get; set; }

        public decimal Total { get; private set; }

        public EnumStatusVenda StatusVenda { get; private set; }

        public DateTime? DataCriacao { get; private set; }

        /// <summary>
        /// Atualiza o status da ordem de venda se for válido.
        /// </summary>
        /// <param name="statusNovo">Novo status a ser inserido</param>
        /// <returns>true para transição válida, false para inválida</returns>
        public bool UpdateStatus(EnumStatusVenda statusNovo)
        {
            //De: "Aguardando pagamento" para: "Pagamento Aprovado" ou "Cancelado"
            // não: enviado para transportadora, entregue, aguardando pagamento.
            if (StatusVenda == EnumStatusVenda.AguardandoPagamento
                && (statusNovo == EnumStatusVenda.PagamentoAprovado
                || statusNovo == EnumStatusVenda.Cancelado))
            {
                StatusVenda = statusNovo;
                return true;
            }
            //De: "Pagamento Aprovado" para: "Enviado para Transportadora" ou "Cancelado"
            //não: aguardando pagamento, paagamento aprovado, entregue
            else if (StatusVenda == EnumStatusVenda.PagamentoAprovado &&
                (statusNovo == EnumStatusVenda.EnviadoParaTransportadora
                || statusNovo == EnumStatusVenda.Cancelado))
            {
                StatusVenda = statusNovo;
                return true;
            }
            //De: "Enviado para Transportador" para: "Entregue"
            //não: aguardando pagamento, pagamento aprovado, enviado para transportadora, cancelado.
            else if (StatusVenda == EnumStatusVenda.EnviadoParaTransportadora
                && statusNovo == EnumStatusVenda.Entregue)
            {
                StatusVenda = statusNovo;
                return true;
            }

            return false;
        }

    }
}
