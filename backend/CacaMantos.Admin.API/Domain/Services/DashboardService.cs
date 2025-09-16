using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.IRepositories;
using CacaMantos.Admin.API.Domain.Services.IServices;

namespace CacaMantos.Admin.API.Domain.Services
{
    public class DashboardService : IDashboardService
    {        
        private readonly ILojaRepository _lojaRepository;
        private readonly ITimeRepository _timeRepository;

        public DashboardService(
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