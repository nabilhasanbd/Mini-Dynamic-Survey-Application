using System.ComponentModel.DataAnnotations;

namespace MneSystem.Application.DTOs;

public class RegisterUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    public string? Designation { get; set; }

    public string? Organization { get; set; }
}