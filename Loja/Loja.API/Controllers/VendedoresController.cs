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
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;
        public VendedoresController(IVendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        /// <summary>
        /// Obtém uma lista de vendedores paginada.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <returns>Retorna um Ok Object Result com a lista de vendedores.</returns>
        [HttpGet]
        public async Task<ActionResult<List<VendedorDTO>>> Get([FromQuery] PagingParameters parameters)
        {
            var pagingList = await _vendedorService.GetVendedores(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
        }

        /// <summary>
        /// Obtém um vendedor pelo Id.
        /// </summary>
        /// <param name="id">Id do vendedor.</param>
        /// <returns>Retorna um Ok Object Result com o vendedor.</returns>
        [HttpGet("{id}", Name = "GetVendedor")]
        public async Task<ActionResult<VendedorDTO>> Get(int id)
        {
            var vendedor = await _vendedorService.GetById(id);

            if (vendedor == null)
                return NotFound();

            return Ok(vendedor);
        }

        /// <summary>
        /// Cria um vendedor.
        /// </summary>
        /// <param name="vendedorDto">Objeto com os dados do vendedor.</param>
        /// <returns>Retorna o vendedor criado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(VendedorDTO vendedorDto)
        {

            var vendedorReturn = await _vendedorService.Add(vendedorDto);

            return new CreatedAtRouteResult("GetVendedor",
                new { id = vendedorDto.Id }, vendedorReturn);
        }

        /// <summary>
        /// Atualiza um vendedor.
        /// </summary>
        /// <param name="id">Id do vendedor.</param>
        /// <param name="vendedorDto">Objeto com os dados atualizados do vendedor.</param>
        /// <returns>Retorna o vendedor atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, VendedorDTO vendedorDto)
        {
            if (id != vendedorDto.Id)
                return BadRequest();

            await _vendedorService.Update(vendedorDto);

            return Ok(vendedorDto);
        }

        /// <summary>
        /// Deleta um vendedor.
        /// </summary>
        /// <param name="id">Id do vendedor.</param>
        /// <returns>Retorna oum Ok Object Result com o vendedor deletado.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<VendedorDTO>> Delete(int id)
        {
            var vendedorDto = await _vendedorService.GetById(id);

            if (vendedorDto == null)
                return NotFound();

            await _vendedorService.Remove(id);
            return Ok(vendedorDto);
        }
    }
}
