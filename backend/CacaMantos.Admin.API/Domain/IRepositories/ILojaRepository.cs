using CacaMantos.Admin.API.Common.DTO;
using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Domain.Pesquisas;

namespace CacaMantos.Admin.API.Domain.IRepositories
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
