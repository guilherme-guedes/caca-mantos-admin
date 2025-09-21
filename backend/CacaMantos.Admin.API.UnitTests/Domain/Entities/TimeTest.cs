using CacaMantos.Admin.API.Domain.Entities;

using Shouldly;

namespace CacaMantos.Admin.API.UnitTests;

public class TimeTest
{
    [Fact]
    public void Deve_Construir_Time()
    {
        var id = Guid.NewGuid();
        var termos = new List<string> { "termo1", "termo2" };
        var homonimos = new List<Time>();
        var time = new Time(id, "Nome", "Identificador", "NomeBusca", termos, true, false, true, homonimos);

        time.Id.ShouldBe(id);
        time.Nome.ShouldBe("Nome");
        time.Identificador.ShouldBe("Identificador");
        time.NomeBusca.ShouldBe("NomeBusca");
        time.Termos.ShouldBe(termos);
        time.Destaque.ShouldBe(true);
        time.Ativo.ShouldBe(false);
        time.Principal.ShouldBe(true);
        time.Homonimos.ShouldBe(homonimos);
    }

    [Fact]
    public void Deve_AdicionarHomonimo()
    {
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", principal: true);
        var homonimo = new Time(Guid.NewGuid(), "Nome2", "Identificador2", "NomeBusca2");

        time.AdicionarHomonimo(homonimo);

        time.Homonimos.ShouldContain(homonimo);
    }

    [Fact]
    public void Nao_Deve_AdicionarHomonimo_TimeNaoPrincipal()
    {
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", principal: false);
        var homonimo = new Time(Guid.NewGuid(), "Nome2", "Identificador2", "NomeBusca2");

        Should.Throw<InvalidOperationException>(() => time.AdicionarHomonimo(homonimo));
    }

    [Fact]
    public void Nao_Deve_AdicionarHomonimo_TimeHomonimoNulo()
    {
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", principal: true);

        Should.Throw<ArgumentNullException>(() => time.AdicionarHomonimo(null));
    }

    [Fact]
    public void Deve_Adicionar_Termo()
    {
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca");
        time.AdicionarTermo("termo");

        time.Termos.ShouldContain("termo");
    }

    [Fact]
    public void Nao_Deve_AdicionarTermo_NuloVazio()
    {
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca");
        time.AdicionarTermo(null);
        time.AdicionarTermo("");

        time.Termos.ShouldBeNull();
    }

    [Fact]
    public void Deve_Retornar_TemTimesHomonimos()
    {
        var homonimos = new List<Time> { new Time(Guid.NewGuid(), "Nome2", "Identificador2", "NomeBusca2") };
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", homonimos: homonimos);

        time.TemTimesHomonimos().ShouldBeTrue();
    }

    [Fact]
    public void Nao_Deve_Retornar_TemTimesHomonimos_VazioNulo()
    {
        var time1 = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", homonimos: null);
        var time2 = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", homonimos: new List<Time>());

        time1.TemTimesHomonimos().ShouldBeFalse();
        time2.TemTimesHomonimos().ShouldBeFalse();
    }

    [Fact]
    public void Deve_Retornar_TemTermos()
    {
        var termos = new List<string> { "termo1" };
        var time = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", termos: termos);

        time.TemTermos().ShouldBeTrue();
    }

    [Fact]
    public void Nao_Deve_Retornar_TemTermos_NuloVazio()
    {
        var time1 = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", termos: null);
        var time2 = new Time(Guid.NewGuid(), "Nome", "Identificador", "NomeBusca", termos: new List<string>());

        time1.TemTermos().ShouldBeFalse();
        time2.TemTermos().ShouldBeFalse();
    }
}
