using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;

namespace Loja.Application.Services
{
    public class OrdemVendedorService : IOrdemVendedorService
    {
        private readonly IOrdemRepository _ordemRepository;
        private readonly IMapper _mapper;
        public OrdemVendedorService(IMapper mapper, IOrdemRepository ordemRepository)
        {
            _ordemRepository = ordemRepository ??
                throw new ArgumentNullException(nameof(ordemRepository));

            _mapper = mapper;
        }

        public async Task<OrdemVendedorDTO> GetById(int? id)
        {
            var ordemVendedorEntity = await _ordemRepository.GetByIdAsync(id);
            return _mapper.Map<OrdemVendedorDTO>(ordemVendedorEntity);
        }

        public async Task<IEnumerable<OrdemVendedorDTO>> GetOrdens()
        {
            var ordemVendedorEntity = await _ordemRepository.GetOrdensAsync();
            return _mapper.Map<IEnumerable<OrdemVendedorDTO>>(ordemVendedorEntity);
        }

        public async Task Add(OrdemVendedorDTO ordemVendedorDto)
        {
            var ordemVendedorEntity = _mapper.Map<Ordem>(ordemVendedorDto);
            await _ordemRepository.CreateAsync(ordemVendedorEntity);
        }

        public async Task Remove(int? id)
        {
            var ordemVendedorEntity = _ordemRepository.GetByIdAsync(id).Result;
            await _ordemRepository.RemoveAsync(ordemVendedorEntity);
        }

        public async Task Update(OrdemVendedorDTO ordemVendedorDto)
        {
            var ordemVendedorEntity = _mapper.Map<Ordem>(ordemVendedorDto);
            await _ordemRepository.UpdateAsync(ordemVendedorEntity);
        }
    }
}
