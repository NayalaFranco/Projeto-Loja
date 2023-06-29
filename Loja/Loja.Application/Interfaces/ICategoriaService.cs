using Loja.Application.DTOs;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<Tuple<IList<CategoriaDTO>, PagingInfo>> GetCategorias(PagingParameters parameters);
        Task<CategoriaDTO> GetById(int? id);
        Task<CategoriaDTO> Add(CategoriaDTO categoriaDto);
        Task Update(CategoriaDTO categoriaDto);
        Task Remove(int? id);
    }
}
