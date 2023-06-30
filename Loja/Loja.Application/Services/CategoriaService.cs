using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        public CategoriaService(IMapper mapper, ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém uma lista paginada de categorias.
        /// </summary>
        /// <param name="parameters">Objeto com os dados de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de categorias e os dados de paginação</returns>
        public async Task<PagingList<CategoriaDTO>> GetCategorias(PagingParameters parameters)
        {
            var pagingList = await _categoriaRepository.GetAsync(parameters);

            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(pagingList.Items);

            var pagingListDto = new PagingList<CategoriaDTO>(categoriasDto, pagingList.PaginationInfo);

            return pagingListDto;
        }

        /// <summary>
        /// Obtém uma categoria pelo Id.
        /// </summary>
        /// <param name="id">Id da categoria.</param>
        /// <returns>Retorna uma CategoriaDTO.</returns>
        public async Task<CategoriaDTO> GetById(int? id)
        {
            var categoriaEntity = await _categoriaRepository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<CategoriaDTO>(categoriaEntity);
        }

        /// <summary>
        /// Adiciona um categoria à tabela do banco de dados.
        /// </summary>
        /// <param name="categoriaDto">Objeto com os dados do categoria a ser adicionado.</param>
        /// <returns>Retorna o categoria adicionado.</returns>
        public async Task<CategoriaDTO> Add(CategoriaDTO categoriaDto)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoriaDto);
            var categoria = await _categoriaRepository.CreateAsync(categoriaEntity);
            return _mapper.Map<CategoriaDTO>(categoria);

        }

        /// <summary>
        /// Atualiza um categoria.
        /// </summary>
        /// <param name="categoriaDto">Objeto com os dados do categoria a ser atualizado.</param>
        /// <returns></returns>
        public async Task Update(CategoriaDTO categoriaDto)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaRepository.UpdateAsync(categoriaEntity);
        }

        /// <summary>
        /// Remove um categoria do banco de dados.
        /// </summary>
        /// <param name="id">Id do categoria.</param>
        /// <returns></returns>
        public async Task Remove(int? id)
        {
            var categoriaEntity = _categoriaRepository.GetByIdAsync(x => x.Id == id).Result;
            await _categoriaRepository.RemoveAsync(categoriaEntity);
        }
    }
}
