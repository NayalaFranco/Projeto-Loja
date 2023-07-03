using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<PagingList<Categoria>> GetCategorias(PagingParameters parameters);
        Task<Categoria> GetById(int? id);
        Task<Categoria> Add(Categoria categoriaNova);
        Task Update(Categoria categoria);
        Task Remove(int? id);
    }
}
