using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;

namespace backend.Domain.Services.IServices
{
    public interface ITimeService
    {    
        Task<Time> Criar(Time time);
        Task<Time> Atualizar(Time time);
        Task<Boolean> Excluir(Guid id);
        Task<Time> Obter(Guid id);
        Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTime pesquisa);    
    }
}