using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using backend;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace CacaMantos.Admin.API.ArchTests;

public class TestesCamadas
{
    private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();

    [Fact]
    public void CamadaAplicacao_NaoPodemChamar_CamadaApresentacao()
    {
        var rule = Classes()
            .That()
            .ResideInNamespace("backend.Application.*")
            .Should()
            .NotDependOnAny(Classes()
                .That()
                .ResideInNamespace("backend.Presentation.*"))                
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }    

    [Fact]
    public void CamadaDominio_NaoPodemChamar_CamadaApresentacao()
    {
        var rule = Classes()
            .That()
            .ResideInNamespace("backend.Domain.*")
            .Should()
            .NotDependOnAny(Classes()
                .That()
                .ResideInNamespace("backend.Presentation.*"))                
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }

    [Fact]
    public void CamadaInfra_NaoPodemChamar_CamadaApresentacao()
    {
        var rule = Classes()
            .That()
            .ResideInNamespace("backend.Infra.*")
            .Should()
            .NotDependOnAny(Classes()
                .That()
                .ResideInNamespace("backend.Presentation.*"))
            .WithoutRequiringPositiveResults();

        rule.Check(Architecture);
    }
}
