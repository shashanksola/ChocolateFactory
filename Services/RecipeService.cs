using ChocolateFactory.Controllers;
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

        public string parseIngredients(List<Ingredient> li)
        {
            string ing = "";

            for (int i = 0; i < li.Count; i++)
            {
                Ingredient ingr = li[i];
                ing += ingr.IngredientName + " " + ingr.Quantity.ToString() + " " + ingr.Unit.ToString() + ",";
            }

            return ing;
        }

        public Recipe getRecipeFromRecipeRequest(RecipeRequest request) {
            Recipe recipe = new()
            {
                Name = request.Name,
                Ingredients = parseIngredients(request.Ingredients),
                QuantityPerBatch = request.QuantityPerBatch,
                Instructions = request.Instructions,
            };

            return recipe;
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
