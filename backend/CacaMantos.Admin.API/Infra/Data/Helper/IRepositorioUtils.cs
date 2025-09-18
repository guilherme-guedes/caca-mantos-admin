using backend.Infra.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CacaMantos.Admin.API.Infra.Data.Helper
{
    public interface IRepositorioUtils
    {
        Task<List<TModel>> CarregarDadosDeIds<TModel>(DbSet<TModel> dbSetDados,
           IList<Guid> ids,
           int tamanhoParticao = 30) where TModel : EntityBaseModel;
    }
}