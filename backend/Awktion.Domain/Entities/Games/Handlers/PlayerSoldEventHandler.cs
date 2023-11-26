using Awktion.Domain.Entities.Players;
using Awktion.Domain.Entities.Users;

namespace Awktion.Domain.Entities.Games.Handlers;

public delegate void PlayerSoldEventHandler(object sender, PlayerSoldEventArgs args);

public class PlayerSoldEventArgs : EventArgs
{
    public User Winner { get; init; }
    public Player SoldPlayer { get; init; }
    public int Amount { get; init; }
}