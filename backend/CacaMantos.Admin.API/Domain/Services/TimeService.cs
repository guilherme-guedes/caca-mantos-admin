using backend.Domain.IRepositories;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Domain.Services.IServices;
using backend.Common.DTO;

namespace backend.Domain.Services
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;

        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<Time> Atualizar(Time time)
        {
            return await _timeRepository.Atualizar(time);
        }

        public async Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTime pesquisa)
        {
            return await _timeRepository.Consultar(pesquisa);
        }

        public async Task<Time> Criar(Time time)
        {
            return await _timeRepository.Criar(time);
        }

        public async Task<bool> Excluir(Guid id)
        {
            return await _timeRepository.Excluir(id);
        }

        public async Task<Time> Obter(Guid id)
        {
            return await _timeRepository.Obter(id);
        }
    }
}