
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
    public required string Username { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public required string PasswordHash { get; set; }

    [Required]
    public required UserRole Role { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string Email { get; set; }

    public bool IsActive { get; set; } = true;
}