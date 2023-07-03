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
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;
        public ProdutosController(IMapper mapper, IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _mapper = mapper;
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

            var produtosDto = _mapper.Map<List<ProdutoDTO>>(pagingList.Items);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(produtosDto);
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

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
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

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
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

            var produtosDto = _mapper.Map<List<ProdutoDTO>>(pagingList.Items);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(produtosDto);
        }

        /// <summary>
        /// Cria um produto.
        /// </summary>
        /// <param name="produtoNovo">Objeto com os dados do produto a ser criado.</param>
        /// <returns>Retorna o produto criado.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(ProdutoDTO produtoNovo)
        {
            var categoria = await _categoriaService.GetById(produtoNovo.CategoriaId);

            if (categoria == null)
                return NotFound($"A categoria id:{produtoNovo.CategoriaId} não foi encontrada!");

            var produtoEntity = _mapper.Map<Produto>(produtoNovo);

            var produtoReturn = await _produtoService.Add(produtoEntity);

            var produtoDto = _mapper.Map<ProdutoDTO>(produtoReturn);

            return new CreatedAtRouteResult("GetProduto",
                new { id = produtoNovo.Id }, produtoDto);
        }

        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="id">Id do produto para confirmação</param>
        /// <param name="produtoUpdate">Objeto com os dados atualizados do produto.</param>
        /// <returns>Retorna um Ok Object Result com o produto atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProdutoDTO produtoUpdate)
        {
            if (id != produtoUpdate.Id)
                return BadRequest("A Id informada e a Id do objeto precisam ser as mesmas.");

            //
            var produto = await _produtoService.GetById(produtoUpdate.Id);
            var categoria = await _categoriaService.GetById(produtoUpdate.CategoriaId);

            if (produto is null)
                return NotFound("O produto a ser editado não foi encontrado!");

            if (categoria is null)
                return NotFound("A id da categoria inserida não foi encontrada!");
            //

            var produtoEntity = _mapper.Map<Produto>(produtoUpdate);

            await _produtoService.Update(produtoEntity);

            return Ok(produtoUpdate);
        }

        /// <summary>
        /// Deleta um produto.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <returns>Retorna um Ok Result.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            await _produtoService.Remove(id);
            return Ok();
        }
    }
}
