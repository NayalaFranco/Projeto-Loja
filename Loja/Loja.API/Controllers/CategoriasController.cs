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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;
        public CategoriasController(IMapper mapper, ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
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

            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(pagingList.Items);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(categoriasDto);

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

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(categoriaDto);
        }

        /// <summary>
        /// Cria uma categoria.
        /// </summary>
        /// <param name="categoriaNova">Objeto com os dados da categoria.</param>
        /// <returns>Retorna a categoria criada.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaNova)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoriaNova);

            var categoriaReturn = await _categoriaService.Add(categoriaEntity);

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoriaReturn);

            return new CreatedAtRouteResult("GetCategoria",
                new { id = categoriaNova.Id }, categoriaDto);
        }

        /// <summary>
        /// Atualiza uma categoria.
        /// </summary>
        /// <param name="id">Id da categoria a ser atualizada para confirmação.</param>
        /// <param name="categoriaUpdate">Objeto CategoriaDTO com os novos dados.</param>
        /// <returns>Retorna um Ok Object Result com a categoria atualizada.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaDTO categoriaUpdate)
        {
            if (id != categoriaUpdate.Id)
                return BadRequest("A Id informada e a Id do objeto precisam ser as mesmas.");

            var categoriaEntity = _mapper.Map<Categoria>(categoriaUpdate);

            await _categoriaService.Update(categoriaEntity);

            return Ok(categoriaUpdate);
        }

        /// <summary>
        /// Deleta uma categoria.
        /// </summary>
        /// <param name="id">Id da categoria.</param>
        /// <returns>Retorna um Ok Result confirmando que a categoria que foi deletada.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            await _categoriaService.Remove(id);
            return Ok();
        }
    }
}
