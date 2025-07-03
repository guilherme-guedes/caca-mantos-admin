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
                    .MapWith(id => TimeFactory.CriarComId(id));

            config.NewConfig<TimeDocumento, Time>()
                    .MapWith(src => new Time(
                        src.Id,
                        src.Nome,
                        src.Identificador,
                        src.NomeBusca,
                        src.Termos,
                        src.Destaque,
                        src.Ativo,
                        src.Principal,
                        src.Homonimos != null ? src.Homonimos.Select(id => id.Adapt<Time>()).ToList() : null
                    ));
        }
    }
}