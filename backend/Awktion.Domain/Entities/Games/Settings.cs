namespace Awktion.Domain.Entities.Games;

public class Settings
{
    public int Budget { get; set; }
    public int TimerMinutes { get; set; }
    public List<Player> AvailablePlayers { get; set; }

}
