using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Infra.Data.Model;

using Mapster;

namespace CacaMantos.Admin.API.Infra.Data.Mapping.Entities
{
    public class LojaMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LojaModel, Loja>()
                    .MapWith(src => new Loja(
                        src.Id,
                        src.Nome,
                        src.Site,
                        src.UrlBusca,
                        src.Parceira,
                        src.Ativa,
                        src.Times != null ? src.Times.Adapt<List<Time>>() : null
                    ));
        }
    }
}
