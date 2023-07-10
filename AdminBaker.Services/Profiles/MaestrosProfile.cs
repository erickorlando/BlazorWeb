using AdminBaker.Entities;
using AdminBaker.Entities.Info;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using AutoMapper;

namespace AdminBaker.Services.Profiles;

public class MaestrosProfile : Profile
{
    public MaestrosProfile()
    {
        CreateMap<TipoTorta, TipoTortaDto>();
        CreateMap<TipoTortaDtoRequest, TipoTorta>();

        CreateMap<UnidadMedida, UnidadMedidaDto>();
        CreateMap<UnidadMedidaDtoRequest, UnidadMedida>();

        CreateMap<MateriaPrima, MateriaPrimaDto>();
        CreateMap<MateriaPrimaInfo, MateriaPrimaDto>();
        CreateMap<MateriaPrimaDtoRequest, MateriaPrima>();

        CreateMap<Receta, RecetaDto>();
        CreateMap<RecetaDtoRequest, Receta>();

        CreateMap<ProductoInfo, ProductoDto>();
        CreateMap<Producto, ProductoDto>();
        CreateMap<ProductoDtoRequest, Producto>();

        CreateMap<PedidoInfo, PedidoDto>()
            .ForMember(d => d.EstadoPedido, o => o.MapFrom(x => x.EstadoPedido.ToString()));
    }
}