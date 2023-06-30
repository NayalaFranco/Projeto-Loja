using Loja.Application.DTOs;
using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<PagingList<ProdutoDTO>> GetProdutos(PagingParameters parameters);
        Task<PagingList<ProdutoDTO>> GetProdutos(PagingParameters parameters, Expression<Func<Produto, bool>> predicate);
        Task<ProdutoDTO> GetById(int? id);
        Task<ProdutoDTO> GetByIdIncludeCategoria(int id);
        Task<ProdutoDTO> Add(ProdutoDTO produtoDto);
        Task Update(ProdutoDTO produtoDto);
        Task Remove(int? id);
    }
}
