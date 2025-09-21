using backend.Application.DTO;

using CacaMantos.Admin.API.Domain.Pesquisas;

using Mapster;

namespace CacaMantos.Admin.API.Application.Mapping.ToDomain
{
    public class PesquisaDTOMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PesquisaPaginadaLojaRequest, PesquisaPaginadaLoja>()
                    .MapWith(src => new PesquisaPaginadaLoja(
                        src.Pagina ?? 1,
                        src.TamanhoPagina ?? 0,
                        src.Trecho,
                        src.Parceira,
                        src.Ativa
                    ));
        }
    }
}
