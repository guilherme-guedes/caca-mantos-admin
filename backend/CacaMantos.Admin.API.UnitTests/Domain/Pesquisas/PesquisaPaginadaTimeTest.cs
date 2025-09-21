using CacaMantos.Admin.API.Domain.Pesquisas;

using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Domain.Pesquisas
{
    public class PesquisaPaginadaTimeTest
    {
        [Fact]
        public void Deve_Construir_PesquisaPaginadaTime()
        {
            var pesquisa = new PesquisaPaginadaTime(2, 10, "abc", true, false, true);

            pesquisa.Trecho.ShouldBe("abc");
            pesquisa.Destaque.ShouldBe(true);
            pesquisa.Ativo.ShouldBe(false);
            pesquisa.Principal.ShouldBe(true);
            pesquisa.Pagina.ShouldBe(2);
            pesquisa.TamanhoPagina.ShouldBe(10);
        }

        [Fact]
        public void Deve_Retornar_TemTrechoInformado()
        {
            var pesquisa = new PesquisaPaginadaTime(trecho: "busca");
            pesquisa.TemTrechoInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemTrechoInformado_NuloOuVazio()
        {
            var pesquisa1 = new PesquisaPaginadaTime(trecho: null);
            var pesquisa2 = new PesquisaPaginadaTime(trecho: "");

            pesquisa1.TemTrechoInformado().ShouldBeFalse();
            pesquisa2.TemTrechoInformado().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_TemDestaqueInformado()
        {
            var pesquisa = new PesquisaPaginadaTime(destaque: true);
            pesquisa.TemDestaqueInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemDestaqueInformado_NaoInformado()
        {
            var pesquisa = new PesquisaPaginadaTime();
            pesquisa.TemDestaqueInformado().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_TemAtivoInformado()
        {
            var pesquisa = new PesquisaPaginadaTime(ativo: false);
            pesquisa.TemAtivoInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemAtivoInformado_Quando_NaoInformado()
        {
            var pesquisa = new PesquisaPaginadaTime();
            pesquisa.TemAtivoInformado().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_TemPrincipalInformado()
        {
            var pesquisa = new PesquisaPaginadaTime(principal: true);
            pesquisa.TemPrincipalInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemPrincipalInformado_Quando_NaoInformado()
        {
            var pesquisa = new PesquisaPaginadaTime();
            pesquisa.TemPrincipalInformado().ShouldBeFalse();
        }
    }
}
