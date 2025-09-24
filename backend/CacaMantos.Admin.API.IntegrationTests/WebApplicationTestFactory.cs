using CacaMantos.Admin.API.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Linq;

namespace CacaMantos.Admin.API.IntegrationTests
{
    public class WebApplicationTestFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder?.ConfigureServices(services =>
            {
                var descriptor = services.Where(d =>
                                            d.ServiceType.FullName?.Contains("ContextoBanco", StringComparison.InvariantCulture) == true ||
                                            d.ServiceType.FullName.Contains("DbContext", StringComparison.InvariantCulture) == true)
                                        .ToImmutableList();
                if (descriptor != null)
                    foreach (var d in descriptor)
                        services.Remove(d);

                services.AddDbContext<ContextoBanco>(options => options.UseInMemoryDatabase("TestDb"));

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ContextoBanco>();
                    db.Database.EnsureCreated();

                    InicializarDados(db).GetAwaiter().GetResult();
                }
            });

            base.ConfigureWebHost(builder);
        }


        private async Task InicializarDados(ContextoBanco db)
        {
            await InicializarTimes(db);
            await InicializarLojas(db);

            await db.SaveChangesAsync();
        }

        private async static Task InicializarTimes(ContextoBanco db)
        {
            await db.Times.AddRangeAsync(new Infra.Data.Model.TimeModel
                {
                    Id = Guid.Parse("6e6e5b37-dbc6-4d9c-8a1a-919ad93e978a"),
                    Nome = "Fluminense",
                    Ativo = true,
                    Destaque = true,
                    Identificador = "fluminense-rj",
                    NomeBusca = "fluminense"
                },
                new Infra.Data.Model.TimeModel
                {
                    Id = Guid.Parse("0c1d4e1c-e89b-4bf2-a198-5273b30a6fff"),
                    Nome = "Botafogo",
                    Ativo = true,
                    Destaque = false,
                    Identificador = "botafogo-rj",
                    NomeBusca = "botafogo"
                }
            );
        }

        private async static Task InicializarLojas(ContextoBanco db)
        {
            await db.Lojas.AddRangeAsync(new Infra.Data.Model.LojaModel
                {
                    Id = Guid.Parse("5cf0ddff-8127-44b7-aefd-846314423575"),
                    Nome = "Fut Classics",
                    Site = "https://futclassics.com.br",
                    UrlBusca = "https://www.futclassics.com.br/search-results?q=@time&type=products&page=@pagina&perPage=30",
                    Ativa = false,
                    Parceira = false,
                    Times = new List<Infra.Data.Model.LojaTimeModel>()
                    {
                        new Infra.Data.Model.LojaTimeModel() { IdTime = Guid.Parse("6e6e5b37-dbc6-4d9c-8a1a-919ad93e978a") }
                    }
                },
                new Infra.Data.Model.LojaModel
                {
                    Id = Guid.Parse("ddcd5d2a-bef9-475c-b297-1255c6fd42c1"),
                    Nome = "Memórias do Esporte",
                    Site = "https://memoriasdoesporteoficial.com.br/",
                    UrlBusca = "https://memoriasdoesporteoficial.com.br/page/@pagina/?s=@time&post_type=product&type_aws=true",
                    Ativa = true,
                    Parceira = true,
                    Times = new List<Infra.Data.Model.LojaTimeModel>()
                    {
                        new Infra.Data.Model.LojaTimeModel() { IdTime = Guid.Parse("6e6e5b37-dbc6-4d9c-8a1a-919ad93e978a") },
                        new Infra.Data.Model.LojaTimeModel() { IdTime = Guid.Parse("0c1d4e1c-e89b-4bf2-a198-5273b30a6fff") }
                    }
                }
            );
        }
    }
}