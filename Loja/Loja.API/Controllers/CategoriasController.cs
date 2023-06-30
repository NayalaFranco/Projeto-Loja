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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Obtém uma lista de categorias paginada.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <returns>Retorna um Ok Object Result com a lista de Categorias.</returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get([FromQuery] PagingParameters parameters)
        {
            var pagingList = await _categoriaService.GetCategorias(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);

        }

        /// <summary>
        /// Obtém uma categoria pelo Id.
        /// </summary>
        /// <param name="id">Id da categoria.</param>
        /// <returns>Retorna um Ok Object Result com a categoria.</returns>
        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        /// <summary>
        /// Cria uma categoria.
        /// </summary>
        /// <param name="categoriaDto">Objeto com os dados da categoria.</param>
        /// <returns>Retorna a categoria criada.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaDto)
        {
            var categoriaReturn = await _categoriaService.Add(categoriaDto);

            return new CreatedAtRouteResult("GetCategoria",
                new { id = categoriaDto.Id }, categoriaReturn);
        }

        /// <summary>
        /// Atualiza uma categoria.
        /// </summary>
        /// <param name="id">Id da categoria a ser atualizada.</param>
        /// <param name="categoriaDto">Objeto CategoriaDTO com os novos dados.</param>
        /// <returns>Retorna um Ok Object Result com a categoria atualizada.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.Id)
                return NoContent();

            await _categoriaService.Update(categoriaDto);
            return Ok(categoriaDto);
        }

        /// <summary>
        /// Deleta uma categoria.
        /// </summary>
        /// <param name="id">Id da categoria.</param>
        /// <returns>Retorna um Ok Object Result com a categoria que foi deletada.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoriaDto = await _categoriaService.GetById(id);
            if (categoriaDto == null)
                return NotFound();

            await _categoriaService.Remove(id);
            return Ok(categoriaDto);
        }
    }
}
