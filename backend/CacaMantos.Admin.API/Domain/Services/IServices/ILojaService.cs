using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;

namespace backend.Domain.Services.IServices
{
    public interface ILojaService
    {        
        Task<Loja> Criar(Loja loja);
        Task<Loja> Atualizar(Loja loja);
        Task<Boolean> Excluir(Guid id);
        Task<Loja> Obter(Guid id);
        Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLoja pesquisa);
    }
}