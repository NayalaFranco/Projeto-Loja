using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Infrastructure.Context;

namespace Loja.Infrastructure.Repositories
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
