using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        public VendedorService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository ??
                throw new ArgumentNullException(nameof(vendedorRepository));
        }

        /// <summary>
        /// Obtém um vendedor pelo Id.
        /// </summary>
        /// <param name="id">Id do vendedor.</param>
        /// <returns>Retorna um objeto Vendedor</returns>
        public async Task<Vendedor> GetById(int? id)
        {
            var vendedorEntity = await _vendedorRepository.GetByIdAsync(x => x.Id == id);
            return vendedorEntity;
        }

        /// <summary>
        /// Obtém uma lista paginada com os vendedores.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de vendedores e os dados de paginação</returns>
        public async Task<PagingList<Vendedor>> GetVendedores(PagingParameters parameters)
        {
            var pagingList = await _vendedorRepository.GetAsync(parameters);

            return pagingList;
        }

        /// <summary>
        /// Adiciona um vendedor à tabela do banco de dados.
        /// </summary>
        /// <param name="vendedorNovo">Objeto com os dados do vendedor a ser adicionado</param>
        /// <returns>Retorna o vendedor adicionado</returns>
        public async Task<Vendedor> Add(Vendedor vendedorNovo)
        {
            var vendedor = await _vendedorRepository.CreateAsync(vendedorNovo);
            return vendedor;
        }

        /// <summary>
        /// Remove um vendedor do banco de dados.
        /// </summary>
        /// <param name="id">Id do vendedor</param>
        public async Task Remove(int? id)
        {
            var vendedorEntity = _vendedorRepository.GetByIdAsync(x => x.Id == id).Result;
            if (vendedorEntity == null)
                return;

            await _vendedorRepository.RemoveAsync(vendedorEntity);
        }

        /// <summary>
        /// Atualiza um vendedor
        /// </summary>
        /// <param name="vendedor">Objeto com os dados do vendedor a ser atualizado.</param>
        public async Task Update(Vendedor vendedor)
        {
            await _vendedorRepository.UpdateAsync(vendedor);
        }
    }
}
