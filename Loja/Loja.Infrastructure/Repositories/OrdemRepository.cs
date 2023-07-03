using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Loja.Infrastructure.Repositories
{
    public class OrdemRepository : Repository<Ordem>, IOrdemRepository
    {
        public OrdemRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Busca ordens específicas de forma assíncrona e inclui a lista de Produtos.
        /// </summary>
        /// <param name="parameters">Parâmetros de paginação.</param>
        /// <param name="orderByExpression">Lambda com a definição de ordenação para a lista.</param>
        /// <param name="predicate">Delegate com os critérios de busca.</param>
        /// <returns>Retorna um objeto PagingList com a lista de ordens e os dados da paginação</returns>
        public async Task<PagingList<Ordem>> GetOrdensAsync(PagingParameters parameters, Expression<Func<Ordem, bool>> predicate)
        {
            var query = Get();

            if (!string.IsNullOrEmpty(parameters.OrderedBy))
                query = query.OrderBy($"{parameters.OrderedBy} {parameters.Direction}");

            var count = await query.Where(predicate).CountAsync();
            var items = await query
                .Where(predicate)
                .Include(o => o.Produtos)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToListAsync();

            var pagingList = new PagingList<Ordem>(items, count, parameters.PageNumber, parameters.PageSize);

            return pagingList;
        }

        /// <summary>
        /// Obtém uma ordem pelo Id e Inclui a lista de produtos com cada produto.
        /// </summary>
        /// <param name="predicate">Delegate com os critérios de busca.</param>
        /// <returns>Retorna uma ordem</returns>
        public async Task<Ordem> GetOrdemByIdIncluiProdutoAsync(Expression<Func<Ordem, bool>> predicate)
        {
            return await _context.Ordens.AsNoTracking()
                .Include(o => o.Produtos)
                .ThenInclude(p => p.Produto)
                .SingleOrDefaultAsync(predicate);
        }
    }
}
