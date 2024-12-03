using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "FactoryManager,ProductionSupervisor")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _service;

        public RecipeController(RecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _service.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] Recipe recipe)
        {
            await _service.AddRecipeAsync(recipe);
            return Ok(recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeAsync(Guid id, [FromBody] Recipe updatedRecipe)
        {
            await _service.UpdateRecipeAsync(id, updatedRecipe);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeAsync(Guid id)
        {
            await _service.DeleteRecipeAsync(id);
            return NoContent();
        }
    }
}