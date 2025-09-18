using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;

namespace backend.Domain.IRepositories
{
    public interface ITimeRepository
    {
        public Task<Time> Criar(Time time);
        public Task<Time> Atualizar(Time time);
        public Task<Boolean> Excluir(Guid id);
        public Task<Time> Obter(Guid id);
        public Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTime pesquisa);
        public Task<int> ObterQuantidadeTimes();
        public Task<List<Time>> Consultar(IList<Guid> ids);
    }
}