using CacaMantos.Admin.API.Domain.IRepositories;

namespace CacaMantos.Admin.API.Application.Services
{
    public class DashboardApplicationService
    {
        private readonly ILojaRepository _lojaRepository;
        private readonly ITimeRepository _timeRepository;

        public DashboardApplicationService(
            ILojaRepository lojaRepository,
            ITimeRepository timeRepository)
        {
            _lojaRepository = lojaRepository;
            _timeRepository = timeRepository;
        }

        public Task<int> ObterQuantidadeLojas()
        {
            return _lojaRepository.ObterQuantidadeLojas();
        }

        public Task<int> ObterQuantidadeTimes()
        {
            return _timeRepository.ObterQuantidadeTimes();
        }
    }
}
