namespace Loja.Domain.Entities
{
    public sealed class Produto : Entity
    {
        public Produto(string nome, string descricao,
            decimal preco, string imagemUrl, int estoque,
            DateTime dataCadastro)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Estoque = estoque;
            DataCadastro = dataCadastro;
        }

        public string? Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public string ImagemUrl { get; private set; }
        public int Estoque { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public void Update(string nome, string descricao,
            decimal preco, string imagemUrl, int estoque,
            DateTime dataCadastro, int categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Estoque = estoque;
            DataCadastro = dataCadastro;
            CategoriaId = categoriaId;
        }
    }
}
