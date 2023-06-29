namespace Loja.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public string Nome { get; protected set; }
    }
}
