using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Infra.Data.Model;
using Mapster;

namespace CacaMantos.Admin.API.Infra.Data.Mapping.Entities
{
    public class TimeMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TimeModel, Time>()
                    .MapWith(src => new Time(
                        src.Id,
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        src.Homonimos != null ? src.Homonimos.Select(h => new Time(
                            h.Id,
                            h.Nome,
                            h.Identificador,
                            h.NomeBusca,
                            h.Termos,
                            h.Destaque,
                            h.Ativo,
                            h.Principal,
                            null,
                            null
                        )).ToList() : null,
                        src.TimePrincipal != null ? new Time(
                            src.TimePrincipal.Id,
                            src.TimePrincipal.Nome,
                            src.TimePrincipal.Identificador,
                            src.TimePrincipal.NomeBusca,
                            src.TimePrincipal.Termos,
                            src.TimePrincipal.Destaque,
                            src.TimePrincipal.Ativo,
                            src.TimePrincipal.Principal,
                            null,
                            null)
                        : null
                    ));

            config.NewConfig<LojaTimeModel, Time>()
                    .MapWith(src => new Time(
                        src.Time.Id,
                        src.Time.Identificador,
                        src.Time.Nome,
                        src.Time.NomeBusca,
                        src.Time.Termos,
                        src.Time.Destaque,
                        src.Time.Ativo,
                        src.Time.Principal,
                        null,
                        null
                    ));
                    
            config.NewConfig<Time, TimeModel>()
                  .MapWith(src => new TimeModel()
                  {
                      Id = src.Id,
                      Nome = src.Nome,
                      Identificador = src.Identificador,
                      NomeBusca = src.NomeBusca,
                      Termos = src.Termos,
                      Destaque = src.Destaque,
                      Ativo = src.Ativo,
                      Principal = src.Principal,
                      Homonimos = src.Homonimos != null ? src.Homonimos.Adapt<List<TimeModel>>() : null,
                      TimePrincipal = src.TimePrincipal != null ? src.TimePrincipal.Adapt<TimeModel>() : null
                  });
        }
    }
}
