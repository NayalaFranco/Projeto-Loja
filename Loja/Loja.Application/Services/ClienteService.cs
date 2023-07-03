using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        /// <summary>
        /// Obtém um cliente pelo Id.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns>Retorna um Cliente</returns>
        public async Task<Cliente> GetById(int? id)
        {
            var clienteEntity = await _clienteRepository.GetByIdAsync(x => x.Id == id);
            return clienteEntity;
        }

        /// <summary>
        /// Obtém uma lista de clientes.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de clientes e os dados de paginação</returns>
        public async Task<PagingList<Cliente>> GetClientes(PagingParameters parameters)
        {
            var pagingList = await _clienteRepository.GetAsync(parameters);

            return pagingList;
        }

        /// <summary>
        /// Adiciona um cliente à tabela do banco de dados.
        /// </summary>
        /// <param name="clienteNovo">Objeto com os dados do cliente a ser adicionado.</param>
        /// <returns>Retorna o cliente adicionado.</returns>
        public async Task<Cliente> Add(Cliente clienteNovo)
        {
            var cliente = await _clienteRepository.CreateAsync(clienteNovo);
            return cliente;
        }

        /// <summary>
        /// Remove um cliente do banco de dados.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        public async Task Remove(int? id)
        {
            var clienteEntity = _clienteRepository.GetByIdAsync(x => x.Id == id).Result;

            if (clienteEntity == null)
                return;

            await _clienteRepository.RemoveAsync(clienteEntity);
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>
        /// <param name="cliente">Objeto com os dados do cliente a ser atualizado.</param>
        public async Task Update(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }
    }
}
