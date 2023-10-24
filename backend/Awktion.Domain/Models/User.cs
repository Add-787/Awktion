using Awktion.Domain.Games;

namespace Awktion.Domain.Models;

public class User
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public int Balances;
    public BidStatus Status = BidStatus.Open;
    public List<(Player, int)> Squad = new();

}
