using Loja.Application.DTOs;

namespace Loja.Application.Interfaces
{
    public interface IOrdemClienteService
    {
        Task<IEnumerable<OrdemClienteDTO>> GetOrdens();
        Task<OrdemClienteDTO> GetById(int? id);
        Task Add(OrdemClienteDTO ordemClienteDto);
        Task Update(OrdemClienteDTO ordemClienteDto);
        Task Remove(int? id);
    }
}
