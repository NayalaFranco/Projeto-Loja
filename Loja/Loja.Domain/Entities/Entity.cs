namespace Loja.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; }
        public string Nome { get; protected set; }

    }
}
