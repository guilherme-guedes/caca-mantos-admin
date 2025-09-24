using System.Globalization;
using Shouldly;

namespace CacaMantos.Admin.API.IntegrationTests.Presentation.Controllers
{
    public class DashboardControllerTest : IClassFixture<WebApplicationTestFactory>
    {
        private readonly HttpClient _client;

        public DashboardControllerTest(WebApplicationTestFactory factory)
        {
            _client = factory?.CreateClient();
        }

        [Fact]
        public async Task Deve_Obter_QuantidadeTimes()
        {
            var response = await _client?.GetAsync("/dashboard/quantidade-times");

            response.EnsureSuccessStatusCode();

            var qtdTimes = Int32.Parse(await response.Content.ReadAsStringAsync(), CultureInfo.InvariantCulture);
            qtdTimes.ShouldBe(2);
        }
        

        [Fact]
        public async Task Deve_Obter_QuantidadeLojas()
        {
            var response = await _client?.GetAsync("/dashboard/quantidade-lojas");

            response.EnsureSuccessStatusCode();

            var qtdTimes = Int32.Parse(await response.Content.ReadAsStringAsync(), CultureInfo.InvariantCulture);
            qtdTimes.ShouldBe(2);
        }
    }
}