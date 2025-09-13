using backend.Application.Services;
using backend.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers
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
        public async Task<IActionResult> Consultar([FromQuery] PesquisaPaginadaLojaRequest request)
        {
            return Ok(await _lojaApplicationService.Consultar(request));
        }        
    }
}