using backend.Application;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("lojas")]
    public class LojaController : Controller
    {   private readonly ILogger<LojaController> _logger;
        private readonly LojaApplicationService _lojaApplicationService;

        public LojaController(ILogger<LojaController> logger,
                              LojaApplicationService lojaApplicationService)
        {
            _logger = logger;
            _lojaApplicationService = lojaApplicationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(string id)
        {
            return Ok(await _lojaApplicationService.Obter(Guid.Parse(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Consultar([FromQuery] int pagina,
                                                    [FromQuery] int quantidade,
                                                    [FromQuery] string trecho,
                                                    [FromQuery] bool? parceira,
                                                    [FromQuery] bool? ativa)
        {
            return Ok(await _lojaApplicationService.Consultar(pagina, tamanhoPagina: quantidade, trecho: trecho, parceira: parceira, ativa: ativa));
        }        
    }
}