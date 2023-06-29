using Loja.Application.DTOs;
using Loja.Domain.Entities;
using Loja.Domain.Enums;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Application.Interfaces
{
    public interface IOrdemService
    {
        Task<Tuple<IList<OrdemDTO>, PagingInfo>> GetOrdens(PagingParameters parameters);
        Task<Tuple<IList<OrdemDTO>, PagingInfo>> GetOrdens(PagingParameters parameters, Expression<Func<Ordem, bool>> predicate);
        Task<OrdemDTO> GetOrdemComProdutosById(int? id);
        Task<OrdemDTO> GetById(int? id);
        Task<OrdemDTO> Add(OrdemDTO ordemDto);
        Task<OrdemDTO> UpdateStatus(int id, EnumStatusVenda statusVenda);
        Task Remove(int? id);
    }
}
