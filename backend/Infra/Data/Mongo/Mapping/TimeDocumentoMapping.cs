using backend.Domain.Factories;
using backend.Domain.Model;
using backend.Infra.Data.Documentos;
using Mapster;

namespace backend.Infra.Data.Mongo.Mapping
{
    public class TimeDocumentoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<string, Time>()
                    .MapWith(id => TimeFactory.CriarComId(Guid.Parse(id)));

            config.NewConfig<TimeDocumento, Time>()
                    .MapWith(src => new Time(
                        Guid.Parse(src.Id),
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        src.Homonimos != null ? src.Homonimos.Select(id => id.Adapt<Time>()).ToList() : null
                    ));

            config.NewConfig<Time, TimeDocumento>()
                    .MapWith(src => new TimeDocumento(
                        src.id.ToString(),
                        src.identificador,
                        src.nome,
                        src.nomeBusca,
                        src.destaque,
                        src.ativo,
                        src.principal,
                        src.termos,
                        src.TemTimesHomonimos() ? src.homonimos.Select(th => th.id.ToString()).ToList() : null
                    ));
        }
    }
}