using System.Runtime.CompilerServices;

namespace Awktion.Application.Dtos.Register;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
