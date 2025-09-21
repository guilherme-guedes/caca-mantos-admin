using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace CacaMantos.Admin.API.ArchTests;

public class CamadasTest
{
    private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();

    [Fact]
    public void CamadaAplicacao_NaoPodemChamar_CamadaApresentacao()
    {
        var rule = Classes()
            .That()
            .ResideInNamespace("CacaMantos.Admin.API.Application.*")
            .Should()
            .NotDependOnAny(Classes()
                .That()
                .ResideInNamespace("CacaMantos.Admin.API.Presentation.*"))
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void CamadaDominio_NaoPodemChamar_CamadaApresentacao()
    {
        var rule = Classes()
            .That()
            .ResideInNamespace("CacaMantos.Admin.API.Domain.*")
            .Should()
            .NotDependOnAny(Classes()
                .That()
                .ResideInNamespace("CacaMantos.Admin.API.Presentation.*"))
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void CamadaInfra_NaoPodemChamar_CamadaApresentacao()
    {
        var rule = Classes()
            .That()
            .ResideInNamespace("CacaMantos.Admin.API.Infra.*")
            .Should()
            .NotDependOnAny(Classes()
                .That()
                .ResideInNamespace("CacaMantos.Admin.API.Presentation.*"))
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }
}
