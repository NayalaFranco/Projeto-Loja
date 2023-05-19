using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;

namespace Loja.Application.Services
{
    public class OrdemClienteService : IOrdemClienteService
    {
        private readonly IOrdemRepository _ordemRepository;
        private readonly IMapper _mapper;
        public OrdemClienteService(IMapper mapper, IOrdemRepository ordemRepository)
        {
            _ordemRepository = ordemRepository ??
                throw new ArgumentNullException(nameof(ordemRepository));

            _mapper = mapper;
        }

        public async Task<OrdemClienteDTO> GetById(int? id)
        {
            var ordemClienteEntity = await _ordemRepository.GetByIdAsync(id);
            return _mapper.Map<OrdemClienteDTO>(ordemClienteEntity);
        }

        public async Task<IEnumerable<OrdemClienteDTO>> GetOrdens()
        {
            var ordemClienteEntity = await _ordemRepository.GetOrdensAsync();
            return _mapper.Map<IEnumerable<OrdemClienteDTO>>(ordemClienteEntity);
        }

        public async Task Add(OrdemClienteDTO ordemClienteDto)
        {
            var ordemClienteEntity = _mapper.Map<Ordem>(ordemClienteDto);
            await _ordemRepository.CreateAsync(ordemClienteEntity);
        }

        public async Task Remove(int? id)
        {
            var ordemClienteEntity = _ordemRepository.GetByIdAsync(id).Result;
            await _ordemRepository.RemoveAsync(ordemClienteEntity);
        }

        public async Task Update(OrdemClienteDTO ordemClienteDto)
        {
            var ordemClienteEntity = _mapper.Map<Ordem>(ordemClienteDto);
            await _ordemRepository.UpdateAsync(ordemClienteEntity);
        }
    }
}
