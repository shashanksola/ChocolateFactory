
using System.ComponentModel.DataAnnotations;

public class RawMaterial
{
    [Key]
    public Guid MaterialId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a positive number.")]
    public required int StockQuantity { get; set; }

    [Required]
    public required Unit Unit { get; set; }

    public DateTime? ExpiryDate { get; set; }

    [Required]
    public required Guid SupplierId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Cost per unit must be greater than 0.")]
    public required decimal CostPerUnit { get; set; }
}
