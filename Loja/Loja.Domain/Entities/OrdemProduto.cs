namespace Loja.Domain.Entities
{
    // Tabela de Junção
    public class OrdemProduto
    {
        public OrdemProduto(int ordemId, int produtoId, string nomeProduto,
            decimal precoUnitario, int quantidade, decimal desconto)
        {
            OrdemId = ordemId;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
            Desconto = desconto;
        }

        public int Id { get; private set; }

        public int OrdemId { get; private set; }
        public Ordem? Ordem { get; set; }

        public int ProdutoId { get; private set; }
        public Produto? Produto { get; set; }

        // Persistência do nome do produto
        // caso seja deletado um dia
        public string NomeProduto { get; private set; }

        // Persistência do valor do momento da compra
        public decimal PrecoUnitario { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Desconto { get; private set; }
    }
}
