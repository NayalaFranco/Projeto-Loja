using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infrastructure.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        private ApplicationDbContext _vendedorContext;
        public VendedorRepository(ApplicationDbContext context)
        {
            _vendedorContext = context;
        }

        public async Task<Vendedor> CreateAsync(Vendedor vendedor)
        {
            _vendedorContext.Add(vendedor);
            await _vendedorContext.SaveChangesAsync();
            return vendedor;
        }

        public async Task<Vendedor> GetByIdAsync(int? id)
        {
            return await _vendedorContext.Vendedores.FindAsync(id);
        }

        public async Task<IEnumerable<Vendedor>> GetVendedoresAsync()
        {
            // Aprimorar depois para paginação
            return await _vendedorContext.Vendedores.ToListAsync();
        }

        public async Task<Vendedor> RemoveAsync(Vendedor vendedor)
        {
            _vendedorContext.Remove(vendedor);
            await _vendedorContext.SaveChangesAsync();
            return vendedor;
        }

        public async Task<Vendedor> UpdateAsync(Vendedor vendedor)
        {
            _vendedorContext.Update(vendedor);
            await _vendedorContext.SaveChangesAsync();
            return vendedor;
        }
    }
}
