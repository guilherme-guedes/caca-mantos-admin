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

        public async Task<Loja> Criar(CriacaoLojaRequest request)
        {
            var timesLoja = await _timeRepositorio.Consultar([.. request.Times.Select(Guid.Parse)]);

            var loja = request.Adapt<Loja>();
            loja.AlterarTimes(timesLoja);

            return await _lojaRepositorio.Criar(loja);
        }

        public async Task<Loja> Atualizar(EdicaoLojaRequest request)
        {
            var timesLoja = await _timeRepositorio.Consultar([.. request.Times.Select(Guid.Parse)]);

            var loja = request.Adapt<Loja>();
            loja.AlterarTimes(timesLoja);

            return await _lojaRepositorio.Atualizar(loja);
        }

        public async Task<bool> Excluir(string id)
        {
            return await _lojaRepositorio.Excluir(new Guid(id));
        }

        public async Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLojaRequest request)
        {
            return await _lojaRepositorio.Consultar(request.Adapt<PesquisaPaginadaLoja>());
        }

        public async Task<Loja> Obter(Guid id)
        {
            return await _lojaRepositorio.Obter(id);
        }
    }
}