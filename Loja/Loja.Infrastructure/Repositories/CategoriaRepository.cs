using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private ApplicationDbContext _categoryContext;
        public CategoriaRepository(ApplicationDbContext context)
        {
            _categoryContext = context;
        }

        public async Task<Categoria> CreateAsync(Categoria category)
        {
            _categoryContext.Add(category);
            await _categoryContext.SaveChangesAsync();
            return category;
        }

        public async Task<Categoria> GetByIdAsync(int? id)
        {
            return await _categoryContext.Categorias.FindAsync(id);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _categoryContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> RemoveAsync(Categoria category)
        {
            _categoryContext.Remove(category);
            await _categoryContext.SaveChangesAsync();
            return category;
        }

        public async Task<Categoria> UpdateAsync(Categoria category)
        {
            _categoryContext.Update(category);
            await _categoryContext.SaveChangesAsync();
            return category;
        }
    }
}
