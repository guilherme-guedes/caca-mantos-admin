using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace CacaMantos.Admin.API.ArchTests
{
    public class ModulosTest
    {
        private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();

        [Fact]
        public void Controladores_NaoPodemChamar_Repositorios()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("CacaMantos.Admin.API.Presentation.Controllers")
                .Should()
                .NotDependOnAny(Classes()
                    .That()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Infra.Data"));

            rule.Check(Architecture);
        }

        [Fact]
        public void Entidades_DevemSerIndependentes()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("CacaMantos.Admin.API.Domain.Entities")
                .Should()
                .OnlyDependOn(Classes()
                    .That()
                    .ResideInNamespaceMatching("System.*")
                    .Or()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Common.*")
                    .Or()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Domain.*"));

            rule.Check(Architecture);
        }

        [Fact]
        public void Controladores_DevemDependerSomenteDe_Aplicacao()
        {
            var rule = Classes()
                .That()
                .ResideInNamespace("CacaMantos.Admin.API.Presentation.Controllers")
                .Should()
                .OnlyDependOn(Classes()
                    .That()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Application")
                    .Or()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Presentation")
                    .Or()
                    .ResideInNamespaceMatching("System")
                    .Or()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Common")
                    .Or()
                    .ResideInNamespaceMatching("CacaMantos.Admin.API.Domain.Entities")
                    .Or()
                    .ResideInNamespaceMatching("Microsoft.AspNetCore")
                    .Or()
                    .ResideInNamespaceMatching("Microsoft.Extensions"));

            rule.Check(Architecture);
        }
    }
}
