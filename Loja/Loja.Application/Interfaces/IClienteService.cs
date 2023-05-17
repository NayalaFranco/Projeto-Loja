using Loja.Application.DTOs;

namespace Loja.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetClientes();
        Task<ClienteDTO> GetById(int? id);
        Task Add(ClienteDTO clienteDto);
        Task Update(ClienteDTO clienteDto);
        Task Remove(int? id);
    }
}
