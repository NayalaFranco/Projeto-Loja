using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        /// <summary>
        /// Obtém uma lista paginada de categorias.
        /// </summary>
        /// <param name="parameters">Objeto com os dados de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de categorias e os dados de paginação</returns>
        public async Task<PagingList<Categoria>> GetCategorias(PagingParameters parameters)
        {
            var pagingList = await _categoriaRepository.GetAsync(parameters);

            return pagingList;
        }

        /// <summary>
        /// Obtém uma categoria pelo Id.
        /// </summary>
        /// <param name="id">Id da categoria.</param>
        /// <returns>Retorna uma Categoria.</returns>
        public async Task<Categoria> GetById(int? id)
        {
            var categoriaEntity = await _categoriaRepository.GetByIdAsync(x => x.Id == id);
            return categoriaEntity;
        }

        /// <summary>
        /// Adiciona um categoria à tabela do banco de dados.
        /// </summary>
        /// <param name="categoriaNova">Objeto com os dados do categoria a ser adicionado.</param>
        /// <returns>Retorna a categoria adicionada.</returns>
        public async Task<Categoria> Add(Categoria categoriaNova)
        {

            var categoria = await _categoriaRepository.CreateAsync(categoriaNova);
            return categoria;

        }

        /// <summary>
        /// Atualiza um categoria.
        /// </summary>
        /// <param name="categoria">Objeto com os dados do categoria a ser atualizado.</param>
        public async Task Update(Categoria categoria)
        {
            await _categoriaRepository.UpdateAsync(categoria);
        }

        /// <summary>
        /// Remove um categoria do banco de dados.
        /// </summary>
        /// <param name="id">Id do categoria.</param>
        public async Task Remove(int? id)
        {
            var categoriaEntity = _categoriaRepository.GetByIdAsync(x => x.Id == id).Result;
            if (categoriaEntity == null)
                return;

            await _categoriaRepository.RemoveAsync(categoriaEntity);
        }
    }
}
