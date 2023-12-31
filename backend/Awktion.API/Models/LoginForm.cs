using System.ComponentModel.DataAnnotations;

namespace Awktion.API.Models;

public class LoginForm
{
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }


}
