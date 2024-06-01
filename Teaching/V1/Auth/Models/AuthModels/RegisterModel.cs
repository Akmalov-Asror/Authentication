using System.ComponentModel.DataAnnotations;

namespace Teaching.V1.Auth.Models.AuthModels;

public class RegisterModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
}
