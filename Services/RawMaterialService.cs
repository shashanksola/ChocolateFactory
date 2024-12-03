using ChocolateFactory.Models;
using ChocolateFactory.Repositories;

namespace ChocolateFactory.Services
{
    public class RawMaterialService
    {
        private readonly RawMaterialRepository _repository;

        public RawMaterialService(RawMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RawMaterial>> GetAllMaterialsAsync()
        {
            return await _repository.GetAllRawMaterialsAsync();
        }

        public async Task AddMaterialAsync(RawMaterial material)
        {
            await _repository.AddRawMaterialAsync(material);
        }
    }
}
