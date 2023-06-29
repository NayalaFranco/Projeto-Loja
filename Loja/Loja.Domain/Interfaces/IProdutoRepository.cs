using Loja.Domain.Entities;

namespace Loja.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> GetByIdIncludeCategoriaAsync(int? id);
    }
}
