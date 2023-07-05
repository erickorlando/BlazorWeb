using AdminBaker.Entities;
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
        CreateMap<MateriaPrimaDtoRequest, MateriaPrima>();

        CreateMap<Receta, RecetaDto>();
        CreateMap<RecetaDtoRequest, Receta>();
    }
}