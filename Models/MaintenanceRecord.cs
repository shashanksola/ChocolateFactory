
using System.ComponentModel.DataAnnotations;

public class MaintenanceRecord
{
    [Key]
    public Guid RecordId { get; set; }

    [Required]
    public required Guid EquipmentId { get; set; }

    [Required]
    public required DateTime MaintenanceDate { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Technician name cannot exceed 100 characters.")]
    public required string Technician { get; set; }

    [StringLength(500, ErrorMessage = "Details cannot exceed 500 characters.")]
    public string? Details { get; set; }

    public DateTime? NextScheduledDate { get; set; }
}
