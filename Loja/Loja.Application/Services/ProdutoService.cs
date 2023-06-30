using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ??
                 throw new ArgumentNullException(nameof(produtoRepository));

            _mapper = mapper;
        }

        /// <summary>
        /// Obtém uma lista paginada com os produtos.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de produtos e os dados de paginação</returns>
        public async Task<PagingList<ProdutoDTO>> GetProdutos(PagingParameters parameters)
        {
            var pagingList = await _produtoRepository.GetAsync(parameters);

            var produtosDto = _mapper.Map<List<ProdutoDTO>>(pagingList.Items);

            var pagingListDto = new PagingList<ProdutoDTO>(produtosDto, pagingList.PaginationInfo);

            return pagingListDto;
        }


        /// <summary>
        /// Obtém uma lista paginada com os produtos.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <param name="predicate">Delegate com o critério de busca.</param>
        /// <returns>Retorna um objeto PagingList com a lista de produtos e os dados de paginação</returns>
        public async Task<PagingList<ProdutoDTO>> GetProdutos(PagingParameters parameters, Expression<Func<Produto, bool>> predicate)
        {
            var pagingList = await _produtoRepository.GetAsync(parameters, predicate);

            var produtosDto = _mapper.Map<List<ProdutoDTO>>(pagingList.Items);

            var pagingListDto = new PagingList<ProdutoDTO>(produtosDto, pagingList.PaginationInfo);

            return pagingListDto;
        }

        /// <summary>
        /// Obtém um produto pelo Id.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Retorna um objeto ProdutoDTO</returns>
        public async Task<ProdutoDTO> GetById(int? id)
        {
            var produtoEntity = await _produtoRepository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }

        /// <summary>
        /// Obtém um produto pelo Id com a sua categoria.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Retorna um objeto ProdutoDTO com a Categoria inclusa.</returns>
        public async Task<ProdutoDTO> GetByIdIncludeCategoria(int id)
        {
            var produtoEntity = await _produtoRepository.GetByIdIncludeCategoriaAsync(id);
            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }

        /// <summary>
        /// Adiciona um produto à tabela do banco de dados.
        /// </summary>
        /// <param name="produtoDto">Objeto com os dados do produto a ser adicionado.</param>
        /// <returns>Retorna o produto adicionado.</returns>
        public async Task<ProdutoDTO> Add(ProdutoDTO produtoDto)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDto);
            var produto = await _produtoRepository.CreateAsync(produtoEntity);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="produtoDto">Objeto com os dados do produto a ser atualizado.</param>
        /// <returns></returns>
        public async Task Update(ProdutoDTO produtoDto)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDto);
            await _produtoRepository.UpdateAsync(produtoEntity);
        }

        /// <summary>
        /// Remove um produto do banco de dados.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <returns></returns>
        public async Task Remove(int? id)
        {
            var produtoEntity = _produtoRepository.GetByIdAsync(x => x.Id == id).Result;
            await _produtoRepository.RemoveAsync(produtoEntity);
        }

        /// <summary>
        /// Converte a string da ordenação para uma expressão lambda.
        /// </summary>
        /// <param name="orderedBy">String com a palavra da ordenação.</param>
        /// <returns>Retorna a expressão lambda a ser usada para ordenar.</returns>
        private static Expression<Func<Produto, object>> SwitchCaseOrderedBy(string orderedBy)
        {
            switch (orderedBy.ToLower())
            {
                case "name":
                case "nome":
                    return x => x.Nome;
                case "preco":
                case "price":
                case "preço":
                    return x => x.Preco;
                case "catid":
                case "categoriaid":
                    return x => x.CategoriaId;
                case "estoque":
                    return x => x.Estoque;
                case "data":
                case "date":
                    return x => x.DataCadastro;
                default:
                    return x => x.Id;
            }
        }
    }
}
