using Loja.Domain.Enums;
using Loja.Domain.Validation;

namespace Loja.Domain.Entities
{
    public sealed class Ordem
    {
        public Ordem(int vendedorId, int clientId,
            List<Produto> produtosList, DateTime dataCriacao)
        {
            ProdutosList = new List<Produto>();

            VendedorId = vendedorId;
            ClienteId = clientId;
            ProdutosList = produtosList;
            StatusVenda = EnumStatusVenda.Aberto;
            DataCriacao = dataCriacao;
        }

        public int Id { get; }


        public int VendedorId { get; private set; }
        public Vendedor Vendedor { get; private set; }

        public int ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }


        public List<Produto> ProdutosList { get; private set; }
        public EnumStatusVenda StatusVenda { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public void UpdateLista(List<Produto> produtosList, EnumStatusVenda statusVendaNovo)
        {
            ValidationUpdateProdutos(statusVendaNovo, produtosList);
        }

        public void UpdateStatus(EnumStatusVenda statusVendaNovo)
        {
            ValidationStatus(StatusVenda, statusVendaNovo);
        }

        private void ValidationStatus(EnumStatusVenda statusAtual, EnumStatusVenda statusNovo)
        {
            //De: "Aberto" para: "Aguardando Pagamento" ou "Cancelado"
            if (statusAtual == EnumStatusVenda.Aberto
                && statusNovo == EnumStatusVenda.AguardandoPagamento
                || statusNovo == EnumStatusVenda.Cancelado)
            {
                StatusVenda = statusNovo;
            }
            //De: "Aguardando pagamento" para: "Pagamento Aprovado" ou "Cancelado"
            else if (statusAtual == EnumStatusVenda.AguardandoPagamento
                && (statusNovo == EnumStatusVenda.PagamentoAprovado
                || statusNovo == EnumStatusVenda.Cancelado))
            {
                StatusVenda = statusNovo;
            }
            //De: "Pagamento Aprovado" para: "Enviado para Transportadora" ou "Cancelado"
            else if (statusAtual == EnumStatusVenda.PagamentoAprovado &&
                (statusNovo == EnumStatusVenda.EnviadoParaTransportadora
                || statusNovo == EnumStatusVenda.Cancelado))
            {
                StatusVenda = statusNovo;
            }
            //De: "Enviado para Transportador" para: "Entregue"
            else if (statusAtual == EnumStatusVenda.EnviadoParaTransportadora
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

        private void ValidationUpdateProdutos(EnumStatusVenda statusVenda, List<Produto> produtosList)
        {
            DomainExceptionValidation.When(statusVenda != EnumStatusVenda.Aberto,
                "O Pedido já foi fechado e portanto não pode mais ser alterado!");
            ProdutosList = produtosList;
        }

    }
}
