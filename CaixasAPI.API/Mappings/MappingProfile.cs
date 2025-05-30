using AutoMapper;
using CaixasAPI.Domain.Dtos;
using CaixasAPI.Domain.Models;

namespace CaixasAPI.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PedidoEntradaDto, Pedido>();
            CreateMap<ProdutoDto, Produto>();
            CreateMap<DimensoesDto, Dimensoes>();

            CreateMap<Pedido, PedidoSaidaDto>();
            CreateMap<Produto, ProdutoNaCaixaDto>();
            CreateMap<Dimensoes, DimensoesDto>();
        }
    }
}