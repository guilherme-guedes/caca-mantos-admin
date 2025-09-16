using backend.Utils;
using Shouldly;

namespace CacaMantos.Admin.API.UnitTests.Utils
{
    public class StringUtilsTest
    {
        [Fact]
        public void Deve_Remover_Acentos()
        {
            var texto = "ação";
            var resultado = StringUtils.RemoverAcentos(texto);
            resultado.ShouldBe("acao");
        }

        [Fact]
        public void Nao_Deve_Remover_Acentos_TextoNuloOuVazio()
        {
            StringUtils.RemoverAcentos(null).ShouldBeEmpty();
            StringUtils.RemoverAcentos("").ShouldBeEmpty();
        }

        [Fact]
        public void Nao_Deve_Remover_Acentos_Sem_Acentos()
        {
            var texto = "abc";
            var resultado = StringUtils.RemoverAcentos(texto);
            resultado.ShouldBe("abc");
        }

        [Fact]
        public void Deve_SanitizarTrechoBusca()
        {
            var texto = "  ação   de   espaço  ";
            var resultado = StringUtils.SanitizarTrechoBusca(texto);
            resultado.ShouldBe("acao de espaco");
        }

        [Fact]
        public void Nao_Deve_SanitizarTrechoBusca_TextoNuloOuVazio()
        {
            StringUtils.SanitizarTrechoBusca(null).ShouldBeEmpty();
            StringUtils.SanitizarTrechoBusca("").ShouldBeEmpty();
        }

        [Fact]
        public void Deve_ReduzirEspacos()
        {
            var texto = "a   b    c";
            var resultado = StringUtils.ReduzirEspacos(texto);
            resultado.ShouldBe("a b c");
        }

        [Fact]
        public void Nao_Deve_ReduzirEspacos_TextoNuloOuVazio()
        {
            StringUtils.ReduzirEspacos(null).ShouldBeEmpty();
            StringUtils.ReduzirEspacos("").ShouldBeEmpty();
        }

        [Fact]
        public void Deve_RemoverEspacosInicioFim()
        {
            var texto = "   teste   ";
            var resultado = StringUtils.RemoverEspacosInicioFim(texto);
            resultado.ShouldBe("teste");
        }

        [Fact]
        public void Nao_Deve_RemoverEspacosInicioFim_TextoNuloOuVazio()
        {
            StringUtils.RemoverEspacosInicioFim(null).ShouldBeEmpty();
            StringUtils.RemoverEspacosInicioFim("").ShouldBeEmpty();
        }

        [Fact]
        public void Nao_Deve_RemoverEspacosInicioFim_SemEspacos()
        {
            var texto = "abc";
            var resultado = StringUtils.RemoverEspacosInicioFim(texto);
            resultado.ShouldBe("abc");
        }
    }
}