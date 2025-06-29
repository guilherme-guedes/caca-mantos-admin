using backend.Domain.Model;
using backend.DTO;

namespace backend.Domain.IRepositories
{
    public interface ITimeRepository
    {
        public Task<Time> Criar(Time time);
        public Task<Time> Atualizar(Time time);
        public Task<Time> Excluir(Time time);
        public Task<Time> Obter(String id);
        public Task<PaginaDTO<Time>> Consultar(
            int pagina = 1,
            int tamanhoPagina = 5,
            String trecho = null,
            bool? destaque = null,
            bool? ativo = null,
            bool? principal = null);
    }
}