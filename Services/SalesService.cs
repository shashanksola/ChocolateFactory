using ChocolateFactory.Models;
using ChocolateFactory.Repositories;

namespace ChocolateFactory.Services
{
    public class SalesService
    {
        private readonly SalesOrderRepository _repository;

        public SalesService(SalesOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllOrdersAsync()
        {
            return await _repository.GetAllOrdersAsync();
        }

        public async Task AddOrderAsync(SalesOrder order)
        {
            await _repository.AddOrderAsync(order);
        }
    }
}
