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
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        /// <summary>
        /// Obtém uma lista de produtos.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <returns>Retorna um Ok Object Result com a lista de produtos.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProdutoDTO>>> Get([FromQuery] PagingParameters parameters)
        {
            var pagingList = await _produtoService.GetProdutos(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
        }

        /// <summary>
        /// Obtém um produto pelo Id.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Retorna um Ok Object Result com o produto.</returns>
        [HttpGet("{id}", Name = "GetProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        /// <summary>
        /// Obtém um produto pelo Id com sua categoria.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <returns>Retorna um Ok Object Result com o produto.</returns>
        [HttpGet("GetProdutoIncluiCategoria/{id}", Name = "GetProdutoIncluiCategoria")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoIncluiCategoria(int id)
        {
            var produto = await _produtoService.GetByIdIncludeCategoria(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        /// <summary>
        /// Obtém uma lista de produtos da mesma categoria.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <param name="categoriaId">Id da categoria.</param>
        /// <returns>Retorna um Ok Object Result com a lista de produtos.</returns>
        [HttpGet("GetByCategoriaId/{categoriaId}", Name = "GetByCategoriaId")]
        public async Task<ActionResult<List<ProdutoDTO>>> GetByCategoriaId([FromQuery] PagingParameters parameters, int categoriaId)
        {
            var pagingList = await _produtoService.GetProdutos(parameters, x => x.CategoriaId == categoriaId);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
        }

        /// <summary>
        /// Cria um produto.
        /// </summary>
        /// <param name="produtoDto">Objeto com os dados do produto a ser criado.</param>
        /// <returns>Retorna o produto criado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(ProdutoDTO produtoDto)
        {
            var categoria = await _categoriaService.GetById(produtoDto.CategoriaId);
            if (categoria == null)
                return NotFound($"A categoria id:{produtoDto.CategoriaId} não foi encontrada!");

            var produtoReturn = await _produtoService.Add(produtoDto);

            return new CreatedAtRouteResult("GetProduto",
                new { id = produtoDto.Id }, produtoReturn);
        }

        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <param name="produtoDto">Objeto com os dados atualizados do produto.</param>
        /// <returns>Retorna um Ok Object Result com o produto atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.Id)
                return BadRequest();

            var produto = await _produtoService.GetById(produtoDto.Id);
            var categoria = await _categoriaService.GetById(produtoDto.CategoriaId);

            if (produto is null)
                return NotFound("O produto a ser editado não foi encontrado!");

            if (categoria is null)
                return NotFound("A id da categoria inserida não foi encontrada!");

            await _produtoService.Update(produtoDto);

            return Ok(produtoDto);
        }

        /// <summary>
        /// Deleta um produto.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <returns>Retorna um Ok Object Result com o produto deletado.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produtoDto = await _produtoService.GetById(id);

            if (produtoDto == null)
                return NotFound();

            await _produtoService.Remove(id);
            return Ok(produtoDto);
        }
    }
}
