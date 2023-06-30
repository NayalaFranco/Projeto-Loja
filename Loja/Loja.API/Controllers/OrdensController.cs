using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Enums;
using Loja.Domain.PaginationEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Loja.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdensController : ControllerBase
    {
        private readonly IOrdemService _ordemService;
        private readonly IProdutoService _produtoService;
        private readonly IClienteService _clienteService;
        private readonly IVendedorService _vendedorService;

        public OrdensController(IOrdemService ordemService, IProdutoService produtoService,
            IClienteService clienteService, IVendedorService vendedorService)
        {
            _ordemService = ordemService;
            _produtoService = produtoService;
            _clienteService = clienteService;
            _vendedorService = vendedorService;
        }

        /// <summary>
        /// Obtém uma Ordem pelo Id.
        /// </summary>
        /// <param name="id">Id da ordem.</param>
        /// <returns>Retorna um Ok Object Result com a ordem.</returns>
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<OrdemDTO>> Get(int id)
        {

            var ordem = await _ordemService.GetOrdemComProdutosById(id);

            if (ordem is null)
                return NotFound();

            return Ok(ordem);
        }

        /// <summary>
        /// Obtém uma lista de ordens paginada.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <returns>Retorna um Ok Object Result com a lista de ordens.</returns>
        [HttpGet]
        public async Task<ActionResult<List<OrdemDTO>>> Get([FromQuery] PagingParameters parameters)
        {
            var pagingList = await _ordemService.GetOrdens(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
        }

        /// <summary>
        /// Obtém uma lista de ordens paginada e filtrada pelo Id do cliente.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <param name="clienteId">Id do cliente.</param>
        /// <returns></returns>
        [HttpGet("Cliente/{clienteId}", Name = "GetOrdensCliente")]
        public async Task<ActionResult<OrdemDTO>> GetOrdensCliente([FromQuery] PagingParameters parameters, int clienteId)
        {
            var pagingList = await _ordemService.GetOrdens(parameters, x => x.ClienteId == clienteId);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
        }

        /// <summary>
        /// Obtém uma lista de ordens paginada e filtrada pelo Id do vendedor.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <param name="vendedorId">Id do vendedor.</param>
        /// <returns></returns>
        [HttpGet("Vendedor/{vendedorId}", Name = "GetOrdensVendedor")]
        public async Task<ActionResult<OrdemDTO>> GetOrdensVendedor([FromQuery] PagingParameters parameters, int vendedorId)
        {
            var pagingList = await _ordemService.GetOrdens(parameters, x => x.VendedorId == vendedorId);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingList.PaginationInfo));

            return Ok(pagingList.Items);
        }

        /// <summary>
        /// Cria uma nova ordem de venda.
        /// </summary>
        /// <param name="ordemDto">Objeto com os dados da ordem a ser criada.</param>
        /// <returns>Retorna a Ordem criada.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(OrdemDTO ordemDto)
        {
            // TODO
            // Jogar essas validações para as camadas de serviço ou domain.
            if (ordemDto.Produtos.IsNullOrEmpty())
                return BadRequest("É necessário ter algum produto na lista.");

            await ValidaVendedorCliente(ordemDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            decimal total = await CalcularTotal(ordemDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //

            ordemDto.Total = total;

            ordemDto.DataCriacao = DateTime.Now;

            var ordemReturn = await _ordemService.Add(ordemDto);

            return new CreatedAtRouteResult("GetById",
                new { id = ordemDto.Id }, ordemReturn);
        }

        /// <summary>
        /// Atualiza apenas o status da Ordem.
        /// </summary>
        /// <param name="id">Id da ordem.</param>
        /// <param name="statusVenda">Status a ser atualizado.</param>
        /// <returns>Retorna um Ok Object Result com a ordem Atualizada.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EnumStatusVenda statusVenda)
        {
            var ordemDto = await _ordemService.UpdateStatus(id, statusVenda);
            return Ok(ordemDto);
        }

        /// <summary>
        /// Deleta uma ordem.
        /// </summary>
        /// <param name="id">Id da ordem.</param>
        /// <returns>Retorna um Ok Object Result com a ordem deletada</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ordem>> Delete(int id)
        {
            var ordemDto = await _ordemService.GetById(id);
            if (ordemDto == null)
                return NotFound();

            await _ordemService.Remove(id);
            return Ok(ordemDto);
        }

        private async Task ValidaVendedorCliente(OrdemDTO ordemDto)
        {
            var vendedor = await _vendedorService.GetById(ordemDto.VendedorId);
            if (vendedor is null)
                ModelState.AddModelError("VendedorId", $"O vendedor de Id {ordemDto.VendedorId} não foi encontrado.");

            var cliente = await _clienteService.GetById(ordemDto.ClienteId);
            if (cliente is null)
                ModelState.AddModelError("ClienteId", $"O cliente de Id {ordemDto.ClienteId} não foi encontrado.");
        }

        private async Task<decimal> CalcularTotal(OrdemDTO ordemDto)
        {
            decimal total = 0m;

            for (int i = 0; i < ordemDto.Produtos.Count; i++)
            {
                var produto = await _produtoService.GetById(ordemDto.Produtos[i].ProdutoId);
                if (produto is null)
                {
                    ModelState.AddModelError($"Produtos[{i}].ProdutoId", $"O produto de id {ordemDto.Produtos[i].ProdutoId} não foi encontrado.");
                    return total;
                }

                ordemDto.Produtos[i].NomeProduto = produto.Nome;
                ordemDto.Produtos[i].PrecoUnitario = produto.Preco;
                decimal val = produto.Preco * ordemDto.Produtos[i].Quantidade;
                total += val - ordemDto.Produtos[i].Desconto;
            }

            return total;
        }
    }
}
