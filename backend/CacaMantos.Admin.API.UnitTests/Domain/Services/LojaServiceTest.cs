using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Domain.Services;
using backend.Domain.IRepositories;
using backend.Common.DTO;
using Moq;
using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Domain.Services
{
    public class LojaServiceTest
    {
        [Fact]
        public async Task Deve_Consultar_Loja()
        {
            var pesquisa = new PesquisaPaginadaLoja();
            var loja = new Loja(Guid.NewGuid(), "Loja 1", "https://loja1.com.br", "https://loja1.com.br/@time", true, true);
            var paginaEsperada = new PaginaDTO<Loja>(
                paginaAtual: 1,
                itensPorPagina: 5,
                total: 1,
                itens: new List<Loja> { loja }
            );

            var repoMock = new Mock<ILojaRepository>();
            repoMock.Setup(r => r.Consultar(It.IsAny<PesquisaPaginadaLoja>())).ReturnsAsync(paginaEsperada);

            var service = new LojaService(repoMock.Object);

            var resultado = await service.Consultar(pesquisa);

            resultado.ShouldBe(paginaEsperada);
            resultado.Itens.ShouldContain(loja);
            resultado.PaginaAtual.ShouldBe(1);
        }

        [Fact]
        public async Task Deve_Obter_Loja()
        {
            var id = Guid.NewGuid();
            var loja = new Loja(id, "Loja 1",  "https://loja1.com.br", "https://loja1.com.br/@time", true, true);

            var repoMock = new Mock<ILojaRepository>();
            repoMock.Setup(r => r.Obter(id)).ReturnsAsync(loja);

            var service = new LojaService(repoMock.Object);

            var resultado = await service.Obter(id);

            resultado.ShouldBe(loja);
            resultado.Id.ShouldBe(id);
            resultado.Nome.ShouldBe("Loja 1");
        }
    }
}