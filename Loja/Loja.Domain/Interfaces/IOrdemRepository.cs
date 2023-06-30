using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Domain.Interfaces
{
    public interface IOrdemRepository : IRepository<Ordem>
    {
        Task<PagingList<Ordem>> GetOrdensAsync(
            PagingParameters parameters, Expression<Func<Ordem, bool>> predicate);
        Task<Ordem> GetOrdemByIdIncluiProdutoAsync(Expression<Func<Ordem, bool>> predicate);
    }
}
