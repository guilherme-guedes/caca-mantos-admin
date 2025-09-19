using backend.Domain.Entities;
using CacaMantos.Admin.API.Application.DTO;
using CacaMantos.Admin.API.Application.DTO.Responses;
using Mapster;

namespace CacaMantos.Admin.API.Application.Mapping.ToDomain
{
    public class TimeMapping: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CriacaoTimeRequest, Time>()
                    .MapWith(src => new Time(
                        Guid.Empty,
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        null,
                        null
                    ));

            config.NewConfig<EdicaoTimeRequest, Time>()
                    .MapWith(src => new Time(
                        src.Id != null ? Guid.Parse(src.Id) : Guid.Empty,
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        null,
                        null
                    ));

            config.NewConfig<Time, TimeResumidoDTO>()
                    .MapWith(src => new TimeResumidoDTO(
                        src.Id.ToString(),
                        src.Identificador
                    ));

            config.NewConfig<Time, TimeResponse>()
                    .MapWith(src => new TimeResponse(
                        src.Id.ToString(),
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        src.Homonimos != null ? src.Homonimos.Adapt<IList<TimeResumidoDTO>>() : null,
                        src.TimePrincipal != null ? src.TimePrincipal.Adapt<TimeResumidoDTO>() : null
                    ));                    
                    
            config.NewConfig<Time, TimeResponse>()
                    .MapWith(src => new TimeResponse(
                        src.Id.ToString(),
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        src.Homonimos != null ? src.Homonimos.Adapt<IList<TimeResumidoDTO>>() : null,
                        src.TimePrincipal != null ? src.TimePrincipal.Adapt<TimeResumidoDTO>() : null
                    ));
        }        
    }
}