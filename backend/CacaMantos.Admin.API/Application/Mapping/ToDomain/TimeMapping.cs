using backend.Domain.Entities;
using CacaMantos.Admin.API.Application.DTO;
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
        }
        
    }
}