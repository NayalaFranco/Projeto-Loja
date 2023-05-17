using AutoMapper;
using Loja.Application.DTOs;
using Loja.Domain.Entities;

namespace Loja.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Vendedor, VendedorDTO>().ReverseMap();
            CreateMap<Ordem, OrdemClienteDTO>().ReverseMap();
            CreateMap<Ordem, OrdemVendedorDTO>().ReverseMap();
        }
    }
}
