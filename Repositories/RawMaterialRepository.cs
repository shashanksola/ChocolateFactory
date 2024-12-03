using ChocolateFactory.Data;
using ChocolateFactory.Models;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactory.Repositories
{
    public interface IRawMaterialRepository
    {
        Task<RawMaterial> GetRawMaterialByIdAsync(int materialId);
        Task<IEnumerable<RawMaterial>> GetAllRawMaterialsAsync();
        Task AddRawMaterialAsync(RawMaterial material);
        Task UpdateRawMaterialAsync(RawMaterial material);
        Task DeleteRawMaterialAsync(int materialId);
    }

    public class RawMaterialRepository : IRawMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public RawMaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RawMaterial> GetRawMaterialByIdAsync(int materialId) =>
            await _context.RawMaterials.FindAsync(materialId);

        public async Task<IEnumerable<RawMaterial>> GetAllRawMaterialsAsync() =>
            await _context.RawMaterials.ToListAsync();

        public async Task AddRawMaterialAsync(RawMaterial material)
        {
            await _context.RawMaterials.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRawMaterialAsync(RawMaterial material)
        {
            _context.RawMaterials.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRawMaterialAsync(int materialId)
        {
            var material = await GetRawMaterialByIdAsync(materialId);
            if (material != null)
            {
                _context.RawMaterials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }
    }

}
