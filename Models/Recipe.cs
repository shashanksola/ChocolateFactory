
using System.ComponentModel.DataAnnotations;

public class Recipe
{
    [Key]
    public Guid RecipeId { get; set; }

    [Required]
    public required Guid ProductId { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Ingredients list cannot exceed 500 characters.")]
    public required string Ingredients { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity per batch must be at least 1.")]
    public required int QuantityPerBatch { get; set; }

    [Required]
    [StringLength(1000, ErrorMessage = "Instructions cannot exceed 1000 characters.")]
    public required string Instructions { get; set; }
}
