using Loja.Domain.Entities;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Domain.Interfaces
{
    public interface IOrdemRepository : IRepository<Ordem>
    {
        Task<Tuple<IList<Ordem>, PagingInfo>> GetOrdensAsync(
            PagingParameters parameters, Expression<Func<Ordem, object>> orderByExpression,
            Expression<Func<Ordem, bool>> predicate);
        Task<Ordem> GetOrdemByIdIncluiProdutoAsync(Expression<Func<Ordem, bool>> predicate);
    }
}
