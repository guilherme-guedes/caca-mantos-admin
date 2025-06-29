using backend.Application;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
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
            return Ok(await _timeApplicationService.Obter(id));
        }

        [HttpGet]
        public async Task<IActionResult> Consultar([FromQuery] int pagina,
                                                    [FromQuery] int quantidade,
                                                    [FromQuery] string trecho,
                                                    [FromQuery] bool? destaque,
                                                    [FromQuery] bool? ativo,
                                                    [FromQuery] bool? principal)
        {
            return Ok(await _timeApplicationService.Consultar(pagina, tamanhoPagina: quantidade, trecho, destaque, ativo, principal));
        }
    }
}