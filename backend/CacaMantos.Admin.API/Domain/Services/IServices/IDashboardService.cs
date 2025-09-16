namespace CacaMantos.Admin.API.Domain.Services.IServices
{
    public interface IDashboardService
    {
        Task<int> ObterQuantidadeLojas();
        Task<int> ObterQuantidadeTimes();
    }
}