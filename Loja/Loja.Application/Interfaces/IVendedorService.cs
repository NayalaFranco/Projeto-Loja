using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Interfaces
{
    public interface IVendedorService
    {
        Task<PagingList<Vendedor>> GetVendedores(PagingParameters parameters);
        Task<Vendedor> GetById(int? id);
        Task<Vendedor> Add(Vendedor vendedorNovo);
        Task Update(Vendedor vendedorDto);
        Task Remove(int? id);
    }
}
