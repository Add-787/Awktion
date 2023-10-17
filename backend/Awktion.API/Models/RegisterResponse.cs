namespace Awktion.API.Models;

public class RegisterResponse
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
