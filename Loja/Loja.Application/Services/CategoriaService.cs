using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

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
        /// <returns>Retorna uma lista de CategoriaDTO.</returns>
        public async Task<Tuple<IList<CategoriaDTO>, PagingInfo>> GetCategorias(PagingParameters parameters)
        {
            Expression<Func<Categoria, Object>> orderByExpression;
            switch (parameters.OrderedBy.ToLower())
            {
                case "name":
                case "nome":
                    orderByExpression = x => x.Nome;
                    break;
                default:
                    orderByExpression = x => x.Id;
                    break;
            }

            var (categorias, pagingInfo) = await _categoriaRepository.GetAsync(parameters, orderByExpression);

            var categoriasDto = _mapper.Map<IList<CategoriaDTO>>(categorias);

            return new Tuple<IList<CategoriaDTO>, PagingInfo>(categoriasDto, pagingInfo);
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
