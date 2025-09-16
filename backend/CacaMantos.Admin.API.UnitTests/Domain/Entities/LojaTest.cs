using backend.Domain.Entities;
using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Domain.Entities
{
    public class LojaTest
    {
        [Fact]
        public void Deve_Construir_Loja()
        {
            var id = Guid.NewGuid();
            var loja = new Loja(id, "Loja 1", "https://loja1.com.br", "https://loja1.com.br/@time", true, true);

            loja.Id.ShouldBe(id);
            loja.Nome.ShouldBe("Loja 1");
            loja.Site.ShouldBe("https://loja1.com.br");
            loja.UrlBusca.ShouldBe("https://loja1.com.br/@time");
            loja.Ativa.ShouldBe(true);
            loja.Parceira.ShouldBe(true);
        }
    }
}