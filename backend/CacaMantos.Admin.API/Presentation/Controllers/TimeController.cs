using backend.Application.Services;
using backend.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using CacaMantos.Admin.API.Application.DTO;

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

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriacaoTimeRequest request)
        {
            return Ok(await _timeApplicationService.Criar(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromBody] EdicaoTimeRequest request)
        {
            return Ok(await _timeApplicationService.Atualizar(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            await _timeApplicationService.Excluir(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(string id)
        {
            return Ok(await _timeApplicationService.Obter(id));
        }

        [HttpGet]
        public async Task<IActionResult> Consultar([FromQuery] PesquisaPaginadaTimeRequest request)
        {
            return Ok(await _timeApplicationService.Consultar(request));
        }
    }
}