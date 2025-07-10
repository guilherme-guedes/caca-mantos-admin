using backend.Domain.Model;
using backend.Infra.Data.Postgre.Model;
using Mapster;

namespace backend.Infra.Data.Postgre.Mapping.Entities
{
    public class LojaMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LojaModel, Loja>()
                    .MapWith(src => new Loja(
                        src.id,
                        src.nome,
                        src.site,
                        src.urlBusca,
                        src.parceira,
                        src.ativa,
                        src.times != null ? src.times.Adapt<List<Time>>() : null
                    ));
        }    
    }
}