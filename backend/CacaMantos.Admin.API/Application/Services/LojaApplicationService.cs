using backend.Application.DTO;
using backend.Common.DTO;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Domain.Services.IServices;
using Mapster;

namespace backend.Application.Services
{
    public class LojaApplicationService
    {
        private readonly ILojaService _lojaService;

        public LojaApplicationService(ILojaService lojaService)
        {
            _lojaService = lojaService;
        }

        public async Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLojaRequest request)
        {
            var pesquisa = request.Adapt<PesquisaPaginadaLoja>();
            return await _lojaService.Consultar(pesquisa);
        }

        public async Task<Loja> Obter(Guid id)
        {
            return await _lojaService.Obter(id);
        }
    }
}