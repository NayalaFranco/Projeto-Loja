namespace Loja.Domain.Entities
{
    public sealed class Categoria : Entity
    {
        public Categoria(string nome, string descricao, string imagemUrl)
        {
            Nome = nome;
            Descricao = descricao;
            ImagemUrl = imagemUrl;
        }
        public string Descricao { get; private set; }
        public string ImagemUrl { get; private set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
