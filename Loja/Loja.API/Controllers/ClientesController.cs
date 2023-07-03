using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Loja.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClientesController(IMapper mapper, IClienteService clienteService)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém uma lista de clientes paginada.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação</param>
        /// <returns>Retorna um Ok Object Result com a lista de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get([FromQuery] PagingParameters parameters)
        {
            var pagingList = await _clienteService.GetClientes(parameters);

            var clientesDto = _mapper.Map<List<ClienteDTO>>(pagingList.Items);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(clientesDto);
        }

        /// <summary>
        /// Obtém um cliente pelo Id.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns>Retorna um Ok Object Result com o Cliente.</returns>
        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _clienteService.GetById(id);

            if (cliente == null)
                return NotFound();

            var clienteDto = _mapper.Map<ClienteDTO>(cliente);

            return Ok(clienteDto);
        }

        /// <summary>
        /// Cria um cliente.
        /// </summary>
        /// <param name="clienteNovo">Objeto com os dados do cliente.</param>
        /// <returns>Retorna o cliente criado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(ClienteDTO clienteNovo)
        {
            var clienteEntity = _mapper.Map<Cliente>(clienteNovo);

            var clienteReturn = await _clienteService.Add(clienteEntity);

            var clienteDto = _mapper.Map<ClienteDTO>(clienteReturn);

            return new CreatedAtRouteResult("GetCliente",
                new { id = clienteNovo.Id }, clienteDto);
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>
        /// <param name="id">Id do cliente para confirmação.</param>
        /// <param name="clienteUpdate">Objeto com os dados do cliente</param>
        /// <returns>Retorna um Ok Object Result com o cliente atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ClienteDTO clienteUpdate)
        {
            if (id != clienteUpdate.Id)
                return BadRequest("A Id informada e a Id do objeto precisam ser as mesmas.");

            var clienteEntity = _mapper.Map<Cliente>(clienteUpdate);

            await _clienteService.Update(clienteEntity);

            return Ok(clienteUpdate);
        }

        /// <summary>
        /// Deleta um cliente.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns>Retorna um Ok Result.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> Delete(int id)
        {
            await _clienteService.Remove(id);
            return Ok();
        }
    }
}
