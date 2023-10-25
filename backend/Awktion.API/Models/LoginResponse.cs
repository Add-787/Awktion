namespace Awktion.API.Models;

public class LoginResponse
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
