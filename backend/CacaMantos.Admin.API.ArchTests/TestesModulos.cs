using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using backend;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace CacaMantos.Admin.API.ArchTests
{
    public class TestesModulos
    {
        private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();

        [Fact]
        public void Controladores_NaoPodemChamar_Repositorios()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Presentation.Controllers")
                .Should()
                .NotDependOnAny(Classes()
                    .That()
                    .ResideInNamespaceMatching("backend.Infra.Data"));

            rule.Check(Architecture);
        }

        [Fact]
        public void Entidades_DevemSerIndependentes()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Domain.Entities")
                .Should()
                .OnlyDependOn(Classes()
                    .That()
                    .ResideInNamespaceMatching("System.*")
                    .Or()
                    .ResideInNamespaceMatching("backend.Domain.*"));

            rule.Check(Architecture);
        }

        [Fact]
        public void Controladores_DevemDependerSomenteDe_Aplicacao()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("backend.Presentation.Controllers")
                .Should()
                .OnlyDependOn(Classes()
                    .That()
                    .ResideInNamespaceMatching("backend.Application")
                    .Or()
                    .ResideInNamespaceMatching("backend.Presentation")
                    .Or()
                    .ResideInNamespaceMatching("System")
                    .Or()
                    .ResideInNamespaceMatching("backend.Common")
                    .Or()
                    .ResideInNamespaceMatching("backend.Domain.Entities")
                    .Or()
                    .ResideInNamespaceMatching("Microsoft.AspNetCore")
                    .Or()
                    .ResideInNamespaceMatching("Microsoft.Extensions"));

            rule.Check(Architecture);
        }
    }
}