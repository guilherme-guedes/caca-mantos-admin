using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using backend;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace CacaMantos.Admin.API.ArchTests 
{
    public class TestesConvencao
    {
        private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();

        [Fact]
        public void Interfaces_DevemComecarComPrefixo_I()
        {
            var rule = Interfaces()
                .Should()
                .HaveNameStartingWith("I");

            rule.Check(Architecture);
        }

        [Fact]
        public void ClassesAbstratas_DevemTer_AbstractOuBase()
        {
            var rule = Classes()
                .That()
                .AreAbstract()
                .Should()
                .HaveNameContaining("Abstract")
                .OrShould()
                .HaveNameContaining("Base");

            rule.Check(Architecture);
        }

        [Fact]
        public void Controladores_DevemTerSufixo()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Presentation.Controllers")
                .Should()
                .HaveNameEndingWith("Controller");

            rule.Check(Architecture);
        }

        [Fact]
        public void Servicos_DevemTerSufixo()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Domain.Services")
                .Or()
                .ResideInNamespace("backend.Domain.IServices")
                .Should()
                .HaveNameEndingWith("Service");

            rule.Check(Architecture);
        }

        [Fact]
        public void Repositorios_DevemTerSufixo()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Infra.Data.Repositories")
                .Or()
                .ResideInNamespace("backend.Domain.Repositories")
                .Should()
                .HaveNameEndingWith("Repository");

            rule.Check(Architecture);
        }
        
        [Fact]
        public void Utils_DevemTerSufixo()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Utils")
                .Should()
                .HaveNameEndingWith("Utils")
                .OrShould()
                .HaveNameEndingWith("Extensions");

            rule.Check(Architecture);
        }
    }
}