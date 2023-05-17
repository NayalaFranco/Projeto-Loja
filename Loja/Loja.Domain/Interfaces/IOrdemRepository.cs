using Loja.Domain.Entities;

namespace Loja.Domain.Interfaces
{
    public interface IOrdemRepository
    {
        Task<IEnumerable<Ordem>> GetOrdensAsync();
        Task<Ordem> GetByIdAsync(int? id);
        Task<Ordem> CreateAsync(Ordem ordem);
        Task<Ordem> UpdateAsync(Ordem ordem);
        Task<Ordem> RemoveAsync(Ordem ordem);
    }
}
