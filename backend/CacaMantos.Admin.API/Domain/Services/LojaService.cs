using backend.Domain.IRepositories;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Domain.Services.IServices;
using backend.Common.DTO;

namespace backend.Domain.Services
{
    public class LojaService : ILojaService
    {        
        private readonly ILojaRepository _lojaRepository;

        public LojaService(ILojaRepository lojaRepository)
        {
            _lojaRepository = lojaRepository;
        }

        public async Task<Loja> Atualizar(Loja loja)
        {
            return await _lojaRepository.Atualizar(loja);
        }

        public async Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLoja pesquisa)
        {
            return await _lojaRepository.Consultar(pesquisa);
        }

        public async Task<Loja> Criar(Loja loja)
        {
            return await _lojaRepository.Criar(loja);
        }

        public async Task<bool> Excluir(Guid id)
        {
            return await _lojaRepository.Excluir(id);
        }

        public async Task<Loja> Obter(Guid id)
        {
            return await _lojaRepository.Obter(id);
        }
    }
}