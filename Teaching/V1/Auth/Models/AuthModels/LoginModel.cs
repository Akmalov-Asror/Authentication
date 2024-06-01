using System.ComponentModel.DataAnnotations;

namespace Teaching.V1.Auth.Models.AuthModels;

public class LoginModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
