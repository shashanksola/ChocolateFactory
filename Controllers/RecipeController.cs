using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> AddRecipe([FromBody] RecipeRequest recipe)
        {
            await _service.AddRecipeAsync(_service.getRecipeFromRecipeRequest(recipe));
            return Ok(recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeAsync(Guid id, [FromBody] RecipeRequest updatedRecipe)
        {   
            
            await _service.UpdateRecipeAsync(id, _service.getRecipeFromRecipeRequest(updatedRecipe));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeAsync(Guid id)
        {
            await _service.DeleteRecipeAsync(id);
            return NoContent();
        }
    }

    public class RecipeRequest
    {
        [Key]
        [Required]
        public required string Name { get; set; }

        public required List<Ingredient> Ingredients { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity per batch must be at least 1.")]
        public required int QuantityPerBatch { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Instructions cannot exceed 1000 characters.")]
        public required string Instructions { get; set; }
    }

    public class Ingredient {
        public required string IngredientName { get; set; }
        public required double Quantity { get; set; }
        public required Unit Unit { get; set; }
    }
}