using Loja.Application.DTOs;

namespace Loja.Application.Interfaces
{
    public interface IOrdemVendedorService
    {
        Task<IEnumerable<OrdemVendedorDTO>> GetOrdens();
        Task<OrdemVendedorDTO> GetById(int? id);
        Task Add(OrdemVendedorDTO ordemVendedorDto);
        Task Update(OrdemVendedorDTO ordemVendedorDto);
        Task Remove(int? id);
    }
}
