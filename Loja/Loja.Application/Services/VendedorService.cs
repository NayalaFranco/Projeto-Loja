using AutoMapper;
using Loja.Application.DTOs;
using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces;

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

        public async Task<VendedorDTO> GetById(int? id)
        {
            var vendedorEntity = await _vendedorRepository.GetByIdAsync(id);
            return _mapper.Map<VendedorDTO>(vendedorEntity);
        }

        public async Task<IEnumerable<VendedorDTO>> GetVendedores()
        {
            var vendedorEntity = await _vendedorRepository.GetVendedoresAsync();
            return _mapper.Map<IEnumerable<VendedorDTO>>(vendedorEntity);
        }

        public async Task Add(VendedorDTO vendedorDto)
        {
            var vendedorEntity = _mapper.Map<Vendedor>(vendedorDto);
            await _vendedorRepository.CreateAsync(vendedorEntity);
        }

        public async Task Remove(int? id)
        {
            var vendedorEntity = _vendedorRepository.GetByIdAsync(id).Result;
            await _vendedorRepository.RemoveAsync(vendedorEntity);
        }

        public async Task Update(VendedorDTO vendedorDto)
        {
            var vendedorEntity = _mapper.Map<Vendedor>(vendedorDto);
            await _vendedorRepository.UpdateAsync(vendedorEntity);
        }
    }
}
