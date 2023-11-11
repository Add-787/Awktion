using Awktion.Domain.Models;

namespace Awktion.Domain.Models;

/// <summary>
/// Class provides the settings that will be used to create the game.
/// </summary>
public class Settings
{
    public TimeSpan TimeSpan { get; set; } = TimeSpan.FromMinutes(1);
    public NoBalance Condition { get; set; } = NoBalance.IncompleteSquad;
    public List<Player> AvailablePlayers { get; set; } = new();
    public int TotalBalance { get; } = 110;
    public int MaxPlayers { get; set; } = 11;

}

/// <summary>
/// Condition applied when a player has no balance.
/// </summary>
public enum NoBalance {
    IncompleteSquad
}
