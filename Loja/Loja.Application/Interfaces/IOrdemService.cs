using Loja.Domain.Entities;
using Loja.Domain.Enums;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Application.Interfaces
{
    public interface IOrdemService
    {
        Task<PagingList<Ordem>> GetOrdens(PagingParameters parameters);
        Task<PagingList<Ordem>> GetOrdens(PagingParameters parameters, Expression<Func<Ordem, bool>> predicate);
        Task<Ordem> GetOrdemComProdutosById(int? id);
        Task<Ordem> GetById(int? id);
        Task<Ordem> Add(Ordem ordemNova);
        Task<Ordem> UpdateStatus(int id, EnumStatusVenda statusVenda);
        Task Remove(int? id);
    }
}
