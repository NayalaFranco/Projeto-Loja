namespace Loja.Domain.Entities
{
    public sealed class Produto : Entity
    {
        public Produto(string nome, string descricao,
            decimal preco, string imagemUrl, int estoque,
            DateTime? dataCadastro, int categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Estoque = estoque;
            DataCadastro = (dataCadastro == null) ? DateTime.Now : dataCadastro;
            CategoriaId = categoriaId;
        }

        public DateTime? DataCadastro { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public string ImagemUrl { get; private set; }
        public int Estoque { get; private set; }
        public int CategoriaId { get; private set; }
        public Categoria? Categoria { get; set; }
        public ICollection<OrdemProduto> Ordens { get; set; }
    }
}
