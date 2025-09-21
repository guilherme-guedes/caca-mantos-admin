using backend.Application.DTO;

using CacaMantos.Admin.API.Application.DTO;
using CacaMantos.Admin.API.Application.DTO.Responses;
using CacaMantos.Admin.API.Common.DTO;
using CacaMantos.Admin.API.Common.Utils;
using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Domain.IRepositories;
using CacaMantos.Admin.API.Domain.Pesquisas;

using Mapster;

namespace CacaMantos.Admin.API.Application.Services
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
            ArgumentNullException.ThrowIfNull(request);

            var timesHomonimos = await _timeRepositorio.Consultar([.. request.Homonimos.Select(Guid.Parse)]).ConfigureAwait(false);
            var timePrincipal = await _timeRepositorio.Obter(request.TimePrincipal != null ? Guid.Parse(request.TimePrincipal) : Guid.Empty).ConfigureAwait(false);

            var time = request.Adapt<Time>();

            if (CollectionsUtils.IsNotNullOrEmpty(timesHomonimos))
                time.AlterarTimesHomonimos(timesHomonimos);

            if (timePrincipal is not null)
                time.AlterarTimePrincipal(timePrincipal);

            var timeCriado = await _timeRepositorio.Criar(time).ConfigureAwait(false);
            return timeCriado.Adapt<TimeResponse>();
        }

        public async Task<TimeResponse> Atualizar(EdicaoTimeRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var timesHomonimos = await _timeRepositorio.Consultar([.. request.Homonimos.Select(Guid.Parse)]).ConfigureAwait(false);
            var timePrincipal = await _timeRepositorio.Obter(request.TimePrincipal != null ? Guid.Parse(request.TimePrincipal) : Guid.Empty).ConfigureAwait(false);

            var time = request.Adapt<Time>();

            if (CollectionsUtils.IsNotNullOrEmpty(timesHomonimos))
                time.AlterarTimesHomonimos(timesHomonimos);

            if (timePrincipal is not null)
                time.AlterarTimePrincipal(timePrincipal);

            var timeAtualizado = await _timeRepositorio.Atualizar(time).ConfigureAwait(false);
            return timeAtualizado.Adapt<TimeResponse>();
        }

        public async Task<Boolean> Excluir(string id)
        {
            return await _timeRepositorio.Excluir(new Guid(id)).ConfigureAwait(false);
        }

        public async Task<TimeResponse> Obter(string id)
        {
            var timeConsultado = await _timeRepositorio.Obter(new Guid(id)).ConfigureAwait(false);
            return timeConsultado?.Adapt<TimeResponse>();
        }

        public async Task<PaginaDTO<TimeResponse>> Consultar(PesquisaPaginadaTimeRequest request)
        {
            var pesquisa = request.Adapt<PesquisaPaginadaTime>();
            var timesconsultados = await _timeRepositorio.Consultar(pesquisa).ConfigureAwait(false);

            return new PaginaDTO<TimeResponse>(
                timesconsultados.PaginaAtual,
                timesconsultados.ItensPorPagina,
                timesconsultados.QuantidadeTotal,
                timesconsultados.Itens.Adapt<List<TimeResponse>>()
            );
        }
    }
}
