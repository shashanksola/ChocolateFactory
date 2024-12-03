using ChocolateFactory.Models;
using ChocolateFactory.Repositories;

namespace ChocolateFactory.Services
{
    public class RecipeService
    {
        private readonly RecipeRepository _repository;

        public RecipeService(RecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _repository.GetAllRecipesAsync();
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _repository.AddRecipeAsync(recipe);
        }

        public async Task UpdateRecipeAsync(Guid id, Recipe updatedRecipe)
        {
            await _repository.UpdateRecipeAsync(id, updatedRecipe);
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            await _repository.DeleteRecipeAsync(id);
        }
    }
}
