using backend.Common.DTO;
using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Common.DTO
{
    public class PaginaDTOTest
    {
        [Fact]
        public void Deve_Construir_PaginaDTO_Corretamente()
        {
            var itens = new List<string> { "a", "b", "c" };
            var pagina = new PaginaDTO<string>(paginaAtual: 2, itensPorPagina: 2, total: 5, itens: itens);

            pagina.PaginaAtual.ShouldBe(2);
            pagina.ItensPorPagina.ShouldBe(2);
            pagina.QuantidadeTotal.ShouldBe(5);
            pagina.TotalPaginas.ShouldBe(3);
            pagina.Itens.ShouldBe(itens);
        }

        [Fact]
        public void Deve_Retornar_PaginaDTO_Vazia()
        {
            var pagina = PaginaDTO<string>.Vazia(paginaAtual: 1, itensPorPagina: 10);

            pagina.PaginaAtual.ShouldBe(1);
            pagina.ItensPorPagina.ShouldBe(10);
            pagina.QuantidadeTotal.ShouldBe(0);
            pagina.TotalPaginas.ShouldBe(0);
            pagina.Itens.ShouldBeEmpty();
        }

        [Fact]
        public void Deve_Calcular_TotalPaginas_Corretamente_DivisaoExata()
        {
            var itens = new List<int> { 1, 2, 3, 4 };
            var pagina = new PaginaDTO<int>(paginaAtual: 1, itensPorPagina: 2, total: 4, itens: itens);

            pagina.TotalPaginas.ShouldBe(2);
        }

        [Fact]
        public void Deve_Calcular_TotalPaginas_Corretamente_DivisaoNaoExata()
        {
            var itens = new List<int> { 1, 2, 3 };
            var pagina = new PaginaDTO<int>(paginaAtual: 1, itensPorPagina: 2, total: 3, itens: itens);

            pagina.TotalPaginas.ShouldBe(2);
        }
    }
}