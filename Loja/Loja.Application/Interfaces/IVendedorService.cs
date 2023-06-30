using Loja.Application.DTOs;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Interfaces
{
    public interface IVendedorService
    {
        Task<PagingList<VendedorDTO>> GetVendedores(PagingParameters parameters);
        Task<VendedorDTO> GetById(int? id);
        Task<VendedorDTO> Add(VendedorDTO vendedorDto);
        Task Update(VendedorDTO vendedorDto);
        Task Remove(int? id);
    }
}
