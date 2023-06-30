using Loja.Application.DTOs;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Interfaces
{
    public interface IClienteService
    {
        Task<PagingList<ClienteDTO>> GetClientes(PagingParameters parameters);
        Task<ClienteDTO> GetById(int? id);
        Task<ClienteDTO> Add(ClienteDTO clienteDto);
        Task Update(ClienteDTO clienteDto);
        Task Remove(int? id);
    }
}
