using backend.Application.Services;
using backend.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using CacaMantos.Admin.API.Application.DTO;

namespace backend.Presentation.Controllers
{
    [Route("lojas")]
    public class LojaController : Controller
    {
        private readonly ILogger<LojaController> _logger;
        private readonly LojaApplicationService _lojaApplicationService;

        public LojaController(ILogger<LojaController> logger,
                              LojaApplicationService lojaApplicationService)
        {
            _logger = logger;
            _lojaApplicationService = lojaApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriacaoLojaRequest request)
        {
            return Ok(await _lojaApplicationService.Criar(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromBody] EdicaoLojaRequest request)
        {
            return Ok(await _lojaApplicationService.Atualizar(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            await _lojaApplicationService.Excluir(id);
            return NoContent();
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
