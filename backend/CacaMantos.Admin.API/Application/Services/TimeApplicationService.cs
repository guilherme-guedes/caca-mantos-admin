using backend.Application.DTO;
using backend.Common.DTO;
using backend.Domain.Entities;
using backend.Domain.IRepositories;
using backend.Domain.Pesquisas;
using CacaMantos.Admin.API.Application.DTO;
using CacaMantos.Admin.API.Application.DTO.Responses;
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

        public async Task<TimeResponse> Criar(CriacaoTimeRequest request)
        {
            var timesHomonimos = await _timeRepositorio.Consultar([.. request.Homonimos.Select(Guid.Parse)]);
            var timePrincipal = await _timeRepositorio.Obter(request.TimePrincipal != null ? Guid.Parse(request.TimePrincipal) : Guid.Empty);

            var time = request.Adapt<Time>();

            if (timesHomonimos.Any())
                time.AlterarTimesHomonimos(timesHomonimos);

            if (timePrincipal is not null)
                time.AlterarTimePrincipal(timePrincipal);

            var timeCriado = await _timeRepositorio.Criar(time);
            return timeCriado.Adapt<TimeResponse>();
        }

        public async Task<TimeResponse> Atualizar(EdicaoTimeRequest request)
        {
            var timesHomonimos = await _timeRepositorio.Consultar([.. request.Homonimos.Select(Guid.Parse)]);
            var timePrincipal = await _timeRepositorio.Obter(request.TimePrincipal != null ? Guid.Parse(request.TimePrincipal) : Guid.Empty);

            var time = request.Adapt<Time>();

            if (timesHomonimos.Any())
                time.AlterarTimesHomonimos(timesHomonimos);

            if (timePrincipal is not null)
                time.AlterarTimePrincipal(timePrincipal);

            var timeAtualizado = await _timeRepositorio.Atualizar(time);
            return timeAtualizado.Adapt<TimeResponse>();
        }

        public async Task<Boolean> Excluir(string id)
        {
            return await _timeRepositorio.Excluir(new Guid(id));
        }

        public async Task<TimeResponse> Obter(string id)
        {
            var timeConsultado = await _timeRepositorio.Obter(new Guid(id));
            return timeConsultado?.Adapt<TimeResponse>();
        }

        public async Task<PaginaDTO<TimeResponse>> Consultar(PesquisaPaginadaTimeRequest request)
        {
            var pesquisa = request.Adapt<PesquisaPaginadaTime>();
            var timesconsultados = await _timeRepositorio.Consultar(pesquisa);
            
            return new PaginaDTO<TimeResponse>(
                timesconsultados.PaginaAtual,
                timesconsultados.ItensPorPagina,
                timesconsultados.QuantidadeTotal,
                timesconsultados.Itens.Adapt<List<TimeResponse>>()
            );
        }
    }
}