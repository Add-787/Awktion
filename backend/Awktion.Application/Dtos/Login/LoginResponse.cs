namespace Awktion.Application.Dtos.Login;

public class LoginResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public bool IsVerified { get; set; }
    public string JwtToken { get; set; }
}
