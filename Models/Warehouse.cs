
using System.ComponentModel.DataAnnotations;

public class Warehouse
{
    [Key]
    public Guid WarehouseId { get; set; }

    [Required]
    [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
    public required string Location { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number.")]
    public required int Capacity { get; set; }

    [Required]
    public required Guid ManagerId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Current stock level cannot be negative.")]
    public int CurrentStockLevel { get; set; }
}
