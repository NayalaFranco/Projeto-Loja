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
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;
        private readonly IMapper _mapper;
        public VendedoresController(IMapper mapper, IVendedorService vendedorService)
        {
            _vendedorService = vendedorService;
            _mapper = mapper;
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

            var vendedoresDto = _mapper.Map<List<VendedorDTO>>(pagingList.Items);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(vendedoresDto);
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

            var vendedorDto = _mapper.Map<VendedorDTO>(vendedor);

            return Ok(vendedorDto);
        }

        /// <summary>
        /// Cria um vendedor.
        /// </summary>
        /// <param name="vendedorNovo">Objeto com os dados do vendedor.</param>
        /// <returns>Retorna o vendedor criado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(VendedorDTO vendedorNovo)
        {
            var vendedorEntity = _mapper.Map<Vendedor>(vendedorNovo);

            var vendedorReturn = await _vendedorService.Add(vendedorEntity);

            var vendedorDto = _mapper.Map<VendedorDTO>(vendedorReturn);

            return new CreatedAtRouteResult("GetVendedor",
                new { id = vendedorNovo.Id }, vendedorDto);
        }

        /// <summary>
        /// Atualiza um vendedor.
        /// </summary>
        /// <param name="id">Id do vendedor para confirmação.</param>
        /// <param name="vendedorUpdate">Objeto com os dados atualizados do vendedor.</param>
        /// <returns>Retorna o vendedor atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, VendedorDTO vendedorUpdate)
        {
            if (id != vendedorUpdate.Id)
                return BadRequest("A Id informada e a Id do objeto precisam ser as mesmas.");

            var vendedorEntity = _mapper.Map<Vendedor>(vendedorUpdate);

            await _vendedorService.Update(vendedorEntity);

            return Ok(vendedorUpdate);
        }

        /// <summary>
        /// Deleta um vendedor.
        /// </summary>
        /// <param name="id">Id do vendedor.</param>
        /// <returns>Retorna um Ok Result.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<VendedorDTO>> Delete(int id)
        {
            await _vendedorService.Remove(id);
            return Ok();
        }
    }
}
