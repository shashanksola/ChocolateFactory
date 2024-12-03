using ChocolateFactory.Models;
using ChocolateFactory.Repositories;

namespace ChocolateFactory.Services
{
    public class PackagingService
    {
        private readonly FinishedGoodsRepository _repository;

        public PackagingService(FinishedGoodsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FinishedGood>> GetAllFinishedGoodsAsync()
        {
            return await _repository.GetAllFinishedGoodsAsync();
        }

        public async Task AddFinishedGoodAsync(FinishedGood finishedGood)
        {
            await _repository.AddFinishedGoodAsync(finishedGood);
        }
    }
}
