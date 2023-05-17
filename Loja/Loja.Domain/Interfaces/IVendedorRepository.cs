using Loja.Domain.Entities;

namespace Loja.Domain.Interfaces
{
    public interface IVendedorRepository
    {
        Task<IEnumerable<Vendedor>> GetVendedoresAsync();
        Task<Vendedor> GetByIdAsync(int? id);
        Task<Vendedor> CreateAsync(Vendedor vendedor);
        Task<Vendedor> UpdateAsync(Vendedor vendedor);
        Task<Vendedor> RemoveAsync(Vendedor vendedor);
    }
}
