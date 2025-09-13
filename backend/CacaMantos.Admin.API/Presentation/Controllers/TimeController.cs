using backend.Application.Services;
using backend.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers
{
    [Route("times")]
    public class TimeController : Controller
    {
        private readonly ILogger<TimeController> _logger;
        private readonly TimeApplicationService _timeApplicationService;

        public TimeController(ILogger<TimeController> logger,
                              TimeApplicationService timeApplicationService)
        {
            _logger = logger;
            _timeApplicationService = timeApplicationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(string id)
        {
            return Ok(await _timeApplicationService.Obter(Guid.Parse(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Consultar([FromQuery] PesquisaPaginadaTimeRequest request)
        {
            return Ok(await _timeApplicationService.Consultar(request));
        }
    }
}