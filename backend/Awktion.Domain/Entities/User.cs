namespace Awktion.Domain.Entities;

public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string ConnectionId { get; set; }
    public List<string> Roles { get; set; }
}