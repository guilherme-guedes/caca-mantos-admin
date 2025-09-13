using backend.Application.DTO;
using backend.Common.DTO;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Domain.Services.IServices;
using Mapster;

namespace backend.Application.Services
{
    public class TimeApplicationService
    {
        private readonly ITimeService _timeService;

        public TimeApplicationService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public async Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTimeRequest request)
        {
            var pesquisa = request.Adapt<PesquisaPaginadaTime>();
            return await _timeService.Consultar(pesquisa);
        }

        public async Task<Time> Obter(Guid id)
        {
            return await _timeService.Obter(id);
        }

        public async Task<Time> Criar(Time time)
        {
            return await _timeService.Criar(time);
        }
        public async Task<Time> Atualizar(Time time)
        {
            return await _timeService.Atualizar(time);            
        }
        
        public async Task<Boolean> Excluir(Guid id)
        {
            return await _timeService.Excluir(id);
        }
    }
}