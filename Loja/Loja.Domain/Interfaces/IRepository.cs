using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<Tuple<IList<T>, PagingInfo>> GetAsync(PagingParameters parameters, Expression<Func<T, object>> orderByExpression);
        Task<Tuple<IList<T>, PagingInfo>> GetAsync(PagingParameters parameters, Expression<Func<T, object>> orderByExpression, Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> RemoveAsync(T entity);
    }
}
