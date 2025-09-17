using backend.Domain.Entities;
using backend.Infra.Data.Model;
using Mapster;

namespace backend.Infra.Data.Mapping.Entities
{
    public class TimeMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TimeModel, Time>()
                    .MapWith(src => new Time(
                        src.id,
                        src.nome,
                        src.identificador,
                        src.nomeBusca,
                        src.termos,
                        src.destaque,
                        src.ativo,
                        src.principal,
                        src.homonimos != null ? src.homonimos.Adapt<List<Time>>() : null,
                        src.timePrincipal != null ? src.timePrincipal.Adapt<Time>() : null
                    ));

            config.NewConfig<LojaTimeModel, Time>()
                    .MapWith(src => new Time(
                        src.time.id,
                        src.time.identificador,
                        src.time.nome,
                        src.time.nomeBusca,
                        src.time.termos,
                        src.time.destaque,
                        src.time.ativo,
                        src.time.principal,
                        null,
                        null
                    ));
            
              config.NewConfig<Time, TimeModel>()
                    .MapWith(src => new TimeModel()
                    {
                        id = src.Id,
                        nome = src.Nome,
                        identificador= src.Identificador,
                        nomeBusca = src.NomeBusca,
                        termos = src.Termos,
                        destaque = src.Destaque,
                        ativo = src.Ativo,
                        principal = src.Principal,
                        homonimos = src.Homonimos != null ? src.Homonimos.Adapt<List<TimeModel>>() : null,
                        timePrincipal = src.TimePrincipal != null ? src.TimePrincipal.Adapt<TimeModel>() : null
                    });
        }
    }
}