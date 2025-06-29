using backend.Domain.IRepositories;
using backend.Domain.Services.IServices;

namespace backend.Domain.Services
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;

        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }
    }
}