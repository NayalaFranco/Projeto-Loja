using Loja.Application.DTOs;

namespace Loja.Application.Interfaces
{
    public interface IVendedorService
    {
        Task<IEnumerable<VendedorDTO>> GetVendedores();
        Task<VendedorDTO> GetById(int? id);
        Task Add(VendedorDTO vendedorDto);
        Task Update(VendedorDTO vendedorDto);
        Task Remove(int? id);
    }
}
