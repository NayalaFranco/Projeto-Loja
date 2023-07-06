using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Loja.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório genérico.
    /// </summary>
    /// <typeparam name="T">Tipo a ser trabalhado.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get base sem tracking.
        /// </summary>
        /// <returns>Retorna um objeto do banco de dados.</returns>
        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Obtém uma lista de objetos do banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com uma lista e os dados da paginação</returns>
        public async Task<PagingList<T>> GetAsync(PagingParameters parameters)
        {
            var query = Get();

            if (!string.IsNullOrEmpty(parameters.OrderedBy))
                query = query.OrderBy($"{parameters.OrderedBy} {parameters.Direction}");

            var count = await query.CountAsync();
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var pagingList = new PagingList<T>(items, count, parameters.PageNumber, parameters.PageSize);

            return pagingList;
        }

        /// <summary>
        /// Obtém os objetos que atendam ao critério.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <param name="predicate">Delegate com o critério de busca.</param>
        /// <returns>Retorna um objeto PagingList com uma lista e os dados da paginação</returns>
        public async Task<PagingList<T>> GetAsync(PagingParameters parameters, Expression<Func<T, bool>> predicate)
        {
            var query = Get();

            if (!string.IsNullOrEmpty(parameters.OrderedBy))
                query = query.OrderBy($"{parameters.OrderedBy} {parameters.Direction}");

            var count = await query.Where(predicate).CountAsync();
            var items = await query
                .Where(predicate)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToListAsync();

            var pagingList = new PagingList<T>(items, count, parameters.PageNumber, parameters.PageSize);

            return pagingList;
        }

        /// <summary>
        /// Obtém um objeto de forma assíncrona pelo seu Id.
        /// </summary>
        /// <param name="predicate">Delegate com o critério de busca</param>
        /// <returns>Retorna o objeto encontrado</returns>
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking()
                .SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Cria um objeto no banco de dados de forma assíncrona 
        /// </summary>
        /// <param name="entity">O objeto a ser criado.</param>
        /// <returns>Retorna o objeto criado.</returns>
        public async Task<T> CreateAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Atualiza um objeto no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="entity">O objeto a ser atualizado.</param>
        /// <returns>Retorna o objeto atualizado.</returns>
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Remove um objeto do banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="entity">O objeto a ser removido.</param>
        /// <returns>Retorna o objeto removido.</returns>
        public async Task<T> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
