using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Domain.Services;
using backend.Domain.IRepositories;
using backend.Common.DTO;
using Moq;
using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Domain.Services
{
    public class TimeServiceTest
    {
        [Fact]
        public async Task Deve_Consultar_Time()
        {
            var pesquisa = new PesquisaPaginadaTime();
            var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca");
            var paginaEsperada = new PaginaDTO<Time>(
                paginaAtual: 1,
                itensPorPagina: 5,
                total: 1,
                itens: new List<Time> { time }
            );

            var repoMock = new Mock<ITimeRepository>();
            repoMock.Setup(r => r.Consultar(It.IsAny<PesquisaPaginadaTime>())).ReturnsAsync(paginaEsperada);

            var service = new TimeService(repoMock.Object);

            var resultado = await service.Consultar(pesquisa);

            resultado.ShouldBe(paginaEsperada);
            resultado.Itens.ShouldContain(time);
            resultado.PaginaAtual.ShouldBe(1);
        }

        [Fact]
        public async Task Deve_Obter_Time()
        {
            var id = Guid.NewGuid();
            var time = new Time(id, "Nome", "Identificador", "NomeBusca");

            var repoMock = new Mock<ITimeRepository>();
            repoMock.Setup(r => r.Obter(id)).ReturnsAsync(time);

            var service = new TimeService(repoMock.Object);

            var resultado = await service.Obter(id);

            resultado.ShouldBe(time);
            resultado.id.ShouldBe(id);
            resultado.nome.ShouldBe("Nome");
        }
    }
}