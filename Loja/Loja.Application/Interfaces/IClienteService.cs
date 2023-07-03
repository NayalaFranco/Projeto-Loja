using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Interfaces
{
    public interface IClienteService
    {
        Task<PagingList<Cliente>> GetClientes(PagingParameters parameters);
        Task<Cliente> GetById(int? id);
        Task<Cliente> Add(Cliente clienteNovo);
        Task Update(Cliente cliente);
        Task Remove(int? id);
    }
}
