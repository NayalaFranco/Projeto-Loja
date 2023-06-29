using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infrastructure.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Obtém um produto pelo id, incluindo sua categoria.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um produto com categoria inclusa.</returns>
        public async Task<Produto> GetByIdIncludeCategoriaAsync(int? id)
        {
            return await _context.Produtos.Include(c => c.Categoria).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
