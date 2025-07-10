using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.DTO;

namespace backend.Application
{
    public class LojaApplicationService
    {
        private readonly ILojaRepository _lojaRepository;

        public LojaApplicationService(ILojaRepository lojaRepository)
        {
            _lojaRepository = lojaRepository;
        }

        public async Task<PaginaDTO<Loja>> Consultar(
            int pagina = 1,
            int tamanhoPagina = 5,
            String trecho = null,
            bool? parceira = null,
            bool? ativa = null)
        {
            return await _lojaRepository.Consultar(pagina, tamanhoPagina, trecho, parceira, ativa);
        }

        public async Task<Loja> Obter(Guid id)
        {
            return await _lojaRepository.Obter(id);
        }
    }
}