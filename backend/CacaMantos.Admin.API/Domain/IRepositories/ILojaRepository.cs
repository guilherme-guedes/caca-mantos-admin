using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;

namespace backend.Domain.IRepositories
{
    public interface ILojaRepository
    {
        Task<Loja> Criar(Loja loja);
        Task<Loja> Atualizar(Loja loja);
        Task<Boolean> Excluir(Guid id);
        Task<Loja> Obter(Guid id);
        Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLoja pesquisa);        
        Task<int> ObterQuantidadeLojas();
    }
}