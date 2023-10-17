using System.ComponentModel.DataAnnotations;

namespace Awktion.API.Models;

public class RegisterForm
{
    [Required(ErrorMessage = "Username is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "The Password and ConfirmPassword should match")]
    public string? ConfirmPassword { get; set; }

}
