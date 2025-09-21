using backend.Application.DTO;

using CacaMantos.Admin.API.Application.DTO;
using CacaMantos.Admin.API.Common.DTO;
using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Domain.IRepositories;
using CacaMantos.Admin.API.Domain.Pesquisas;

using Mapster;

namespace CacaMantos.Admin.API.Application.Services
{
    public class LojaApplicationService
    {
        private readonly ILojaRepository _lojaRepositorio;
        private readonly ITimeRepository _timeRepositorio;

        public LojaApplicationService(
            ILojaRepository lojaRepositorio,
            ITimeRepository timeRepositorio)
        {
            _lojaRepositorio = lojaRepositorio;
            _timeRepositorio = timeRepositorio;
        }

        public async Task<LojaResponse> Criar(CriacaoLojaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var timesLoja = await _timeRepositorio.Consultar([.. request.Times.Select(Guid.Parse)]).ConfigureAwait(false);

            var loja = request.Adapt<Loja>();
            loja.AlterarTimes(timesLoja);

            var lojaCriada = await _lojaRepositorio.Criar(loja).ConfigureAwait(false);
            return lojaCriada.Adapt<LojaResponse>();
        }

        public async Task<LojaResponse> Atualizar(EdicaoLojaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var timesLoja = await _timeRepositorio.Consultar([.. request.Times.Select(Guid.Parse)]).ConfigureAwait(false);

            var loja = request.Adapt<Loja>();
            loja.AlterarTimes(timesLoja);

            var lojaAtualizada = await _lojaRepositorio.Atualizar(loja).ConfigureAwait(false);
            return lojaAtualizada.Adapt<LojaResponse>();
        }

        public async Task<bool> Excluir(string id)
        {
            return await _lojaRepositorio.Excluir(new Guid(id)).ConfigureAwait(false);
        }

        public async Task<PaginaDTO<LojaResponse>> Consultar(PesquisaPaginadaLojaRequest request)
        {
            var lojasConsultadas = await _lojaRepositorio.Consultar(request.Adapt<PesquisaPaginadaLoja>()).ConfigureAwait(false);

            return new PaginaDTO<LojaResponse>(
                lojasConsultadas.PaginaAtual,
                lojasConsultadas.ItensPorPagina,
                lojasConsultadas.QuantidadeTotal,
                lojasConsultadas.Itens.Adapt<List<LojaResponse>>()
            );
        }

        public async Task<LojaResponse> Obter(Guid id)
        {
            var lojaConsultada = await _lojaRepositorio.Obter(id).ConfigureAwait(false);
            return lojaConsultada?.Adapt<LojaResponse>();
        }
    }
}
