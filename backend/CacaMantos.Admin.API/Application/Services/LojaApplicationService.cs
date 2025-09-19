using backend.Application.DTO;
using backend.Common.DTO;
using backend.Domain.Entities;
using backend.Domain.IRepositories;
using backend.Domain.Pesquisas;
using CacaMantos.Admin.API.Application.DTO;
using Mapster;

namespace backend.Application.Services
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
            var timesLoja = await _timeRepositorio.Consultar([.. request.Times.Select(Guid.Parse)]);

            var loja = request.Adapt<Loja>();
            loja.AlterarTimes(timesLoja);

            var lojaCriada = await _lojaRepositorio.Criar(loja);
            return lojaCriada.Adapt<LojaResponse>();
        }

        public async Task<LojaResponse> Atualizar(EdicaoLojaRequest request)
        {
            var timesLoja = await _timeRepositorio.Consultar([.. request.Times.Select(Guid.Parse)]);

            var loja = request.Adapt<Loja>();
            loja.AlterarTimes(timesLoja);

            var lojaAtualizada = await _lojaRepositorio.Atualizar(loja);
            return lojaAtualizada.Adapt<LojaResponse>();
        }

        public async Task<bool> Excluir(string id)
        {
            return await _lojaRepositorio.Excluir(new Guid(id));
        }

        public async Task<PaginaDTO<LojaResponse>> Consultar(PesquisaPaginadaLojaRequest request)
        {
            var lojasConsultadas = await _lojaRepositorio.Consultar(request.Adapt<PesquisaPaginadaLoja>());
         
            return new PaginaDTO<LojaResponse>(
                lojasConsultadas.PaginaAtual,
                lojasConsultadas.ItensPorPagina,
                lojasConsultadas.QuantidadeTotal,
                lojasConsultadas.Itens.Adapt<List<LojaResponse>>()
            );
        }

        public async Task<LojaResponse> Obter(Guid id)
        {
            var lojaConsultada = await _lojaRepositorio.Obter(id);
            return lojaConsultada?.Adapt<LojaResponse>();
        }
    }
}