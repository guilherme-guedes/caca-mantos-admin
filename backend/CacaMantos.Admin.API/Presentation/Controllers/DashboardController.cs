using CacaMantos.Admin.API.Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace CacaMantos.Admin.API.Presentation.Controllers
{
    [Route("dashboard")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly DashboardApplicationService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger,
                              DashboardApplicationService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [HttpGet("quantidade-lojas")]
        public async Task<IActionResult> ObterQuantidadeLojas()
        {
            return Ok(await _dashboardService.ObterQuantidadeLojas().ConfigureAwait(false));
        }

        [HttpGet("quantidade-times")]
        public async Task<IActionResult> ObterQuantidadeTimes()
        {
            return Ok(await _dashboardService.ObterQuantidadeTimes().ConfigureAwait(false));
        }
    }
}
