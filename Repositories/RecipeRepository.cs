using ChocolateFactory.Data;
using ChocolateFactory.Models;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactory.Repositories
{

    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeByIdAsync(Guid recipeId);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Guid id, Recipe recipe);
        Task DeleteRecipeAsync(Guid recipeId);
    }


    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recipe> GetRecipeByIdAsync(Guid recipeId) =>
            await _context.Recipes.FindAsync(recipeId);

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync() =>
            await _context.Recipes.ToListAsync();

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Guid id, Recipe recipe)
        {
            var recipes = await GetRecipeByIdAsync(id);

            if (recipes == null) {
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRecipeAsync(Guid recipeId)
        {
            var recipe = await GetRecipeByIdAsync(recipeId);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }

}
