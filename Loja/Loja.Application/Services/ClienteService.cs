using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteService(IMapper mapper, IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));

            _mapper = mapper;
        }

        /// <summary>
        /// Obtém um cliente pelo Id.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns>Retorna um ClienteDTO</returns>
        public async Task<ClienteDTO> GetById(int? id)
        {
            var clienteEntity = await _clienteRepository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<ClienteDTO>(clienteEntity);
        }

        /// <summary>
        /// Obtém uma lista de clientes.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de clientes e os dados de paginação</returns>
        public async Task<PagingList<ClienteDTO>> GetClientes(PagingParameters parameters)
        {
            var pagingList = await _clienteRepository.GetAsync(parameters);

            var clientesDto = _mapper.Map<List<ClienteDTO>>(pagingList.Items);

            var pagingListDto = new PagingList<ClienteDTO>(clientesDto, pagingList.PaginationInfo);

            return pagingListDto;
        }

        /// <summary>
        /// Adiciona um cliente à tabela do banco de dados.
        /// </summary>
        /// <param name="clienteDto">Objeto com os dados do cliente a ser adicionado.</param>
        /// <returns>Retorna o cliente adicionado.</returns>
        public async Task<ClienteDTO> Add(ClienteDTO clienteDto)
        {
            var clienteEntity = _mapper.Map<Cliente>(clienteDto);
            var cliente = await _clienteRepository.CreateAsync(clienteEntity);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        /// <summary>
        /// Remove um cliente do banco de dados.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns></returns>
        public async Task Remove(int? id)
        {
            var clienteEntity = _clienteRepository.GetByIdAsync(x => x.Id == id).Result;
            await _clienteRepository.RemoveAsync(clienteEntity);
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>
        /// <param name="clienteDto">Objeto com os dados do cliente a ser atualizado.</param>
        /// <returns></returns>
        public async Task Update(ClienteDTO clienteDto)
        {
            var clienteEntity = _mapper.Map<Cliente>(clienteDto);
            await _clienteRepository.UpdateAsync(clienteEntity);
        }
    }
}
