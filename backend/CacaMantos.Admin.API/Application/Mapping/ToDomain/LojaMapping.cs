using backend.Domain.Entities;
using CacaMantos.Admin.API.Application.DTO;
using Mapster;

namespace CacaMantos.Admin.API.Application.Mapping.ToDomain
{
    public class LojaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CriacaoLojaRequest, Loja>()
                    .MapWith(src => new Loja(
                        Guid.Empty,
                        src.Nome,
                        src.Site,
                        src.UrlBusca,
                        src.Parceira,
                        src.Ativa,
                        null
                    ));

            config.NewConfig<EdicaoLojaRequest, Loja>()
                    .MapWith(src => new Loja(
                        src.Id != null ? Guid.Parse(src.Id) : Guid.Empty,
                        src.Nome,
                        src.Site,
                        src.UrlBusca,
                        src.Parceira,
                        src.Ativa,
                        null
                    ));

            config.NewConfig<Loja, LojaResponse>()
                    .MapWith(src => new LojaResponse(
                        src.Id.ToString(),
                        src.Nome,
                        src.Site,
                        src.UrlBusca,
                        src.Parceira,
                        src.Ativa,
                        src.Times.Adapt<IList<TimeResumidoDTO>>()
                    ));
        }
    }
}