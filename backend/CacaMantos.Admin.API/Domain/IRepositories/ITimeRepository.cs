using CacaMantos.Admin.API.Common.DTO;
using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Domain.Pesquisas;

namespace CacaMantos.Admin.API.Domain.IRepositories
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
