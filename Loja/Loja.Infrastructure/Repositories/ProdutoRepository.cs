using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private ApplicationDbContext _produtoContext;
        public ProdutoRepository(ApplicationDbContext context)
        {
            _produtoContext = context;
        }

        public async Task<Produto> CreateAsync(Produto produto)
        {
            _produtoContext.Add(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> GetByIdAsync(int? id)
        {
            return await _produtoContext.Produtos.Include(c => c.Categoria)
               .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            // Aprimorar depois para paginação
            return await _produtoContext.Produtos.ToListAsync();
        }

        public async Task<Produto> RemoveAsync(Produto produto)
        {
            _produtoContext.Remove(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            _produtoContext.Update(produto);
            await _produtoContext.SaveChangesAsync();
            return produto;
        }
    }
}
