using backend.Domain.Entities;
using backend.Infra.Data.Model;
using Mapster;

namespace backend.Infra.Data.Mapping.Entities
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