using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;

namespace Loja.Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMapper _mapper;
        public VendedorService(IMapper mapper, IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository ??
                throw new ArgumentNullException(nameof(vendedorRepository));

            _mapper = mapper;
        }

        /// <summary>
        /// Obtém um vendedor pelo Id.
        /// </summary>
        /// <param name="id">Id do vendedor.</param>
        /// <returns>Retorna um objeto VendedorDTO</returns>
        public async Task<VendedorDTO> GetById(int? id)
        {
            var vendedorEntity = await _vendedorRepository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<VendedorDTO>(vendedorEntity);
        }

        /// <summary>
        /// Obtém uma lista paginada com os vendedores.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de vendedores e os dados de paginação</returns>
        public async Task<PagingList<VendedorDTO>> GetVendedores(PagingParameters parameters)
        {
            var pagingList = await _vendedorRepository.GetAsync(parameters);

            var vendedorDto = _mapper.Map<List<VendedorDTO>>(pagingList.Items);

            var pagingListDto = new PagingList<VendedorDTO>(vendedorDto, pagingList.PaginationInfo);

            return pagingListDto;
        }

        /// <summary>
        /// Adiciona um vendedor à tabela do banco de dados.
        /// </summary>
        /// <param name="vendedorDto">Objeto com os dados do vendedor a ser adicionado</param>
        /// <returns>Retorna o vendedor adicionado</returns>
        public async Task<VendedorDTO> Add(VendedorDTO vendedorDto)
        {
            var vendedorEntity = _mapper.Map<Vendedor>(vendedorDto);
            var vendedor = await _vendedorRepository.CreateAsync(vendedorEntity);
            return _mapper.Map<VendedorDTO>(vendedor);
        }

        /// <summary>
        /// Remove um vendedor do banco de dados.
        /// </summary>
        /// <param name="id">Id do vendedor</param>
        /// <returns></returns>
        public async Task Remove(int? id)
        {
            var vendedorEntity = _vendedorRepository.GetByIdAsync(x => x.Id == id).Result;
            await _vendedorRepository.RemoveAsync(vendedorEntity);
        }

        /// <summary>
        /// Atualiza um vendedor
        /// </summary>
        /// <param name="vendedorDto">Objeto com os dados do vendedor a ser atualizado.</param>
        /// <returns></returns>
        public async Task Update(VendedorDTO vendedorDto)
        {
            var vendedorEntity = _mapper.Map<Vendedor>(vendedorDto);
            await _vendedorRepository.UpdateAsync(vendedorEntity);
        }
    }
}
