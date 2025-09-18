using backend.Application.DTO;
using backend.Common.DTO;
using backend.Domain.Entities;
using backend.Domain.IRepositories;
using backend.Domain.Pesquisas;
using CacaMantos.Admin.API.Application.DTO;
using Mapster;

namespace backend.Application.Services
{
    public class TimeApplicationService
    {
        private readonly ITimeRepository _timeRepositorio;

        public TimeApplicationService(ITimeRepository timeRepositorio)
        {
            _timeRepositorio = timeRepositorio;
        }

        public async Task<Time> Criar(CriacaoTimeRequest request)
        {
            var timesHomonimos = await _timeRepositorio.Consultar([.. request.Homonimos.Select(Guid.Parse)]);
            var timePrincipal = await _timeRepositorio.Obter(request.TimePrincipal != null ? Guid.Parse(request.TimePrincipal) : Guid.Empty);

            var time = request.Adapt<Time>();

            if (timesHomonimos.Any())
                time.AlterarTimesHomonimos(timesHomonimos);

            if (timePrincipal is not null)
                time.AlterarTimePrincipal(timePrincipal);

            return await _timeRepositorio.Criar(time);
        }

        public async Task<Time> Atualizar(EdicaoTimeRequest request)
        {
            var timesHomonimos = await _timeRepositorio.Consultar([.. request.Homonimos.Select(Guid.Parse)]);
            var timePrincipal = await _timeRepositorio.Obter(request.TimePrincipal != null ? Guid.Parse(request.TimePrincipal) : Guid.Empty);

            var time = request.Adapt<Time>();

            if (timesHomonimos.Any())
                time.AlterarTimesHomonimos(timesHomonimos);

            if (timePrincipal is not null)
                time.AlterarTimePrincipal(timePrincipal);


            return await _timeRepositorio.Atualizar(time);
        }

        public async Task<Boolean> Excluir(string id)
        {
            return await _timeRepositorio.Excluir(new Guid(id));
        }

        public async Task<Time> Obter(string id)
        {
            return await _timeRepositorio.Obter(new Guid(id));
        }
        
        public async Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTimeRequest request)
        {
            var pesquisa = request.Adapt<PesquisaPaginadaTime>();
            return await _timeRepositorio.Consultar(pesquisa);
        }
    }
}