using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<PagingList<Produto>> GetProdutos(PagingParameters parameters);
        Task<PagingList<Produto>> GetProdutos(PagingParameters parameters, Expression<Func<Produto, bool>> predicate);
        Task<Produto> GetById(int? id);
        Task<Produto> GetByIdIncludeCategoria(int id);
        Task<Produto> Add(Produto produtoNovo);
        Task Update(Produto produto);
        Task Remove(int? id);
    }
}
