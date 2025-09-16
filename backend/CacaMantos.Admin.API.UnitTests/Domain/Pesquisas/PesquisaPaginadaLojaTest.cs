using backend.Domain.Pesquisas;
using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Domain.Pesquisas
{
    public class PesquisaPaginadaLojaTest
    {
        [Fact]
        public void Deve_Construir_PesquisaPaginadaLoja()
        {
            var pesquisa = new PesquisaPaginadaLoja(3, 15, "loja", true, false);

            pesquisa.Trecho.ShouldBe("loja");
            pesquisa.Parceira.ShouldBe(true);
            pesquisa.Ativo.ShouldBe(false);
            pesquisa.Pagina.ShouldBe(3);
            pesquisa.TamanhoPagina.ShouldBe(15);
        }

        [Fact]
        public void Deve_Retornar_TemTrechoInformado()
        {
            var pesquisa = new PesquisaPaginadaLoja(trecho: "busca");
            pesquisa.TemTrechoInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemTrechoInformado_NuloOuVazio()
        {
            var pesquisa1 = new PesquisaPaginadaLoja(trecho: null);
            var pesquisa2 = new PesquisaPaginadaLoja(trecho: "");

            pesquisa1.TemTrechoInformado().ShouldBeFalse();
            pesquisa2.TemTrechoInformado().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_TemParceiraInformado()
        {
            var pesquisa = new PesquisaPaginadaLoja(parceira: true);
            pesquisa.TemParceiraInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemParceiraInformado()
        {
            var pesquisa = new PesquisaPaginadaLoja();
            pesquisa.TemParceiraInformado().ShouldBeFalse();
        }

        [Fact]
        public void Deve_Retornar_TemAtivoInformado()
        {
            var pesquisa = new PesquisaPaginadaLoja(ativo: false);
            pesquisa.TemAtivoInformado().ShouldBeTrue();
        }

        [Fact]
        public void Nao_Deve_Retornar_TemAtivoInformado()
        {
            var pesquisa = new PesquisaPaginadaLoja();
            pesquisa.TemAtivoInformado().ShouldBeFalse();
        }
    }
}