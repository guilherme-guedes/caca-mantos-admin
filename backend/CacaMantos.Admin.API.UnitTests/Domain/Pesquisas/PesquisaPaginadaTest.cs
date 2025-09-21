using CacaMantos.Admin.API.Domain.Pesquisas;

using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Domain.Pesquisas
{
    public class PesquisaPaginadaTest
    {
        [Fact]
        public void Deve_Construir_PesquisaPaginada_Com_Valores_Padrao()
        {
            var pesquisa = new PesquisaPaginada();

            pesquisa.Pagina.ShouldBe(1);
            pesquisa.TamanhoPagina.ShouldBe(5);
        }

        [Fact]
        public void Deve_Construir_PesquisaPaginada_Com_Valores_Informados()
        {
            var pesquisa = new PesquisaPaginada(3, 20);

            pesquisa.Pagina.ShouldBe(3);
            pesquisa.TamanhoPagina.ShouldBe(20);
        }

        [Fact]
        public void Deve_Ajustar_Pagina_Para_Valor_Minimo()
        {
            var pesquisa = new PesquisaPaginada(0, 10);

            pesquisa.Pagina.ShouldBe(1);
            pesquisa.TamanhoPagina.ShouldBe(10);
        }

        [Fact]
        public void Deve_Ajustar_TamanhoPagina_Para_Valor_Minimo()
        {
            var pesquisa = new PesquisaPaginada(2, 0);

            pesquisa.Pagina.ShouldBe(2);
            pesquisa.TamanhoPagina.ShouldBe(5);
        }
    }
}
