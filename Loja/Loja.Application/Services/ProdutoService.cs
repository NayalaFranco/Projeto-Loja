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
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ??
                 throw new ArgumentNullException(nameof(produtoRepository));
        }

        /// <summary>
        /// Obtém uma lista paginada com os produtos.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de produtos e os dados de paginação</returns>
        public async Task<PagingList<Produto>> GetProdutos(PagingParameters parameters)
        {
            var pagingList = await _produtoRepository.GetAsync(parameters);

            return pagingList;
        }


        /// <summary>
        /// Obtém uma lista paginada com os produtos.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <param name="predicate">Delegate com o critério de busca.</param>
        /// <returns>Retorna um objeto PagingList com a lista de produtos e os dados de paginação</returns>
        public async Task<PagingList<Produto>> GetProdutos(PagingParameters parameters, Expression<Func<Produto, bool>> predicate)
        {
            var pagingList = await _produtoRepository.GetAsync(parameters, predicate);

            return pagingList;
        }

        /// <summary>
        /// Obtém um produto pelo Id.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Retorna um objeto Produto</returns>
        public async Task<Produto> GetById(int? id)
        {
            var produtoEntity = await _produtoRepository.GetByIdAsync(x => x.Id == id);
            return produtoEntity;
        }

        /// <summary>
        /// Obtém um produto pelo Id com a sua categoria.
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Retorna um objeto Produto com a Categoria inclusa.</returns>
        public async Task<Produto> GetByIdIncludeCategoria(int id)
        {
            var produtoEntity = await _produtoRepository.GetByIdIncludeCategoriaAsync(id);
            return produtoEntity;
        }

        /// <summary>
        /// Adiciona um produto à tabela do banco de dados.
        /// </summary>
        /// <param name="produtoNovo">Objeto com os dados do produto a ser adicionado.</param>
        /// <returns>Retorna o produto adicionado.</returns>
        public async Task<Produto> Add(Produto produtoNovo)
        {
            var produto = await _produtoRepository.CreateAsync(produtoNovo);
            return produto;
        }

        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="produto">Objeto com os dados do produto a ser atualizado.</param>
        public async Task Update(Produto produto)
        {
            await _produtoRepository.UpdateAsync(produto);
        }

        /// <summary>
        /// Remove um produto do banco de dados.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        public async Task Remove(int? id)
        {
            var produtoEntity = _produtoRepository.GetByIdAsync(x => x.Id == id).Result;
            if (produtoEntity == null)
                return;
            await _produtoRepository.RemoveAsync(produtoEntity);
        }
    }
}
