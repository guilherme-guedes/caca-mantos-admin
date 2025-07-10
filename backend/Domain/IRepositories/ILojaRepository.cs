using backend.Domain.Model;
using backend.DTO;

namespace backend.Domain.IRepositories
{
    public interface ILojaRepository
    {        
        public Task<Loja> Criar(Loja loja);
        public Task<Loja> Atualizar(Loja loja);
        public Task<Boolean> Excluir(Guid id);
        public Task<Loja> Obter(Guid id);
        public Task<PaginaDTO<Loja>> Consultar(
            int pagina = 1,
            int tamanhoPagina = 5,
            String trecho = null,
            bool? parceira = null,
            bool? ativo = null);
    }
}