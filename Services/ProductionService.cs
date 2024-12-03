using ChocolateFactory.Models;
using ChocolateFactory.Repositories;

namespace ChocolateFactory.Services
{
    public class ProductionService
    {
        private readonly ProductionScheduleRepository _repository;

        public ProductionService(ProductionScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductionSchedule>> GetAllSchedulesAsync()
        {
            return await _repository.GetAllSchedulesAsync();
        }

        public async Task AddScheduleAsync(ProductionSchedule schedule)
        {
            await _repository.AddScheduleAsync(schedule);
        }
    }
}
