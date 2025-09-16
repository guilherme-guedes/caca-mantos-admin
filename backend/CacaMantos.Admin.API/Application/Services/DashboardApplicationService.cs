using CacaMantos.Admin.API.Domain.Services.IServices;

namespace CacaMantos.Admin.API.Application.Services
{
    public class DashboardApplicationService
    {
        private readonly IDashboardService _dashboardService;

        public DashboardApplicationService(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<int> ConsultarQuantidadeLojas()
        {
            return await _dashboardService.ObterQuantidadeLojas();
        }        

        public async Task<int> ConsultarQuantidadeTimes()
        {
            return await _dashboardService.ObterQuantidadeTimes();
        }
    }
}