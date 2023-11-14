using Awktion.Domain.Entities.Players;

namespace Awktion.Domain.Entities.Games;

public class Settings
{
    public int Budget { get; set; }
    public int TimerMinutes { get; set; }
    public List<int> AvailablePlayerIds { get; set; } = new();

}
