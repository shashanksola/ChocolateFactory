using ChocolateFactory.Models;
using ChocolateFactory.Repositories;

namespace ChocolateFactory.Services
{
    public class RawMaterialService
    {
        private readonly IRawMaterialRepository _repository;

        public RawMaterialService(IRawMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<RawMaterial> GetRawMaterialByNameAsync(string materialName)
        {
            return await _repository.GetRawMaterialByNameAsync(materialName);
        }

        public async Task<RawMaterial> GetRawMaterialByBatchIdAsync(Guid materialBatchId)
        {
            return await _repository.GetRawMaterialByBatchIdAsync(materialBatchId);
        }

        public async Task<IEnumerable<RawMaterial>> GetAllRawMaterialsAsync()
        {
            return await _repository.GetAllRawMaterialsAsync();
        }

        public async Task<IEnumerable<RawMaterial>> GetAllRawMaterialsByWarehouseNameAsync(string warehouseName)
        {
            return await _repository.GetAllRawMaterialsByWarehouseNameAsync(warehouseName);
        }

        public async Task AddRawMaterialAsync(RawMaterial material)
        {
            await _repository.AddRawMaterialAsync(material);
        }

        public async Task UpdateRawMaterialAsync(RawMaterial material)
        {
            await _repository.UpdateRawMaterialAsync(material);
        }

        public async Task DeleteRawMaterialAsync(string name)
        {
            await _repository.DeleteRawMaterialAsync(name);
        }

        public async Task DeleteRawMaterialByBatchAsync(Guid materialBatchId)
        {
            await _repository.DeleteRawMaterialByBatchAsync(materialBatchId);
        }
    }
}
