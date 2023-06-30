using Loja.Application.DTOs;
using Loja.Application.Interfaces;
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
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
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

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
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

            return Ok(cliente);
        }

        /// <summary>
        /// Cria um cliente.
        /// </summary>
        /// <param name="clienteDto">Objeto com os dados do cliente.</param>
        /// <returns>Retorna o cliente criado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(ClienteDTO clienteDto)
        {

            var clienteReturn = await _clienteService.Add(clienteDto);

            return new CreatedAtRouteResult("GetCliente",
                new { id = clienteDto.Id }, clienteReturn);
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <param name="clienteDto">Objeto com os dados do cliente</param>
        /// <returns>Retorna um Ok Object Result com o cliente atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ClienteDTO clienteDto)
        {
            if (id != clienteDto.Id)
                return BadRequest();

            await _clienteService.Update(clienteDto);

            return Ok(clienteDto);
        }

        /// <summary>
        /// Deleta um cliente.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns>Retorna um Ok Object Result com o cliente deletado.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> Delete(int id)
        {
            var clienteDto = await _clienteService.GetById(id);

            if (clienteDto == null)
                return NotFound();

            await _clienteService.Remove(id);
            return Ok(clienteDto);
        }
    }
}
