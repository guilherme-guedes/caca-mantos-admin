using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.Domain.Services.IServices;
using backend.DTO;

namespace backend.Application
{
    public class TimeApplicationService
    {
        private readonly ITimeService _timeService;
        private readonly ITimeRepository _timeRepository;

        public TimeApplicationService(ITimeService timeService,
                                    ITimeRepository timeRepository)
        {
            _timeService = timeService;
            _timeRepository = timeRepository;
        }

        public async Task<PaginaDTO<Time>> Consultar(
            int pagina = 1,
            int tamanhoPagina = 5,
            String trecho = null,
            bool? destaque = null,
            bool? ativo = null,
            bool? principal = null)
        {
            return await _timeRepository.Consultar(pagina, tamanhoPagina, trecho, destaque, ativo, principal);
        }

        public async Task<Time> Obter(Guid id)
        {
            return await _timeRepository.Obter(id);
        }

        public async Task<Time> Criar(Time time)
        {
            return await _timeRepository.Criar(time);
        }
        public async Task<Time> Atualizar(Time time)
        {
            return await _timeRepository.Atualizar(time);            
        }
        
        public async Task<Boolean> Excluir(Guid id)
        {
            return await _timeRepository.Excluir(id);
        }
    }
}