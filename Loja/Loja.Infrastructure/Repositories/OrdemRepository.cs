using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Loja.Infrastructure.Repositories
{
    public class OrdemRepository : IOrdemRepository
    {
        private ApplicationDbContext _ordemContext;
        public OrdemRepository(ApplicationDbContext context)
        {
            _ordemContext = context;
        }

        public async Task<Ordem> CreateAsync(Ordem ordem)
        {
            _ordemContext.Add(ordem);
            await _ordemContext.SaveChangesAsync();
            return ordem;
        }

        public async Task<Ordem> GetByIdAsync(int? id)
        {
            return await _ordemContext.Ordens
                .Include(v => v.Vendedor)
                .Include(c => c.Cliente)
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Ordem>> GetOrdensAsync()
        {
            // Aprimorar depois para paginação
            return await _ordemContext.Ordens.ToListAsync();
        }

        public async Task<Ordem> RemoveAsync(Ordem ordem)
        {
            _ordemContext.Remove(ordem);
            await _ordemContext.SaveChangesAsync();
            return ordem;
        }

        public async Task<Ordem> UpdateAsync(Ordem ordem)
        {
            _ordemContext.Update(ordem);
            await _ordemContext.SaveChangesAsync();
            return ordem;
        }
    }
}
