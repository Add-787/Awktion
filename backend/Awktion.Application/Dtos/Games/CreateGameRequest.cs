using Awktion.Application.Dtos.Players;
using Awktion.Domain.Entities;

namespace Awktion.Application.Dtos.Games;

public class CreateGameRequest
{
    public int RoomId { get; set; }
    public int TimerMinutes { get; set; }
    public int Budget { get; set; }
    public List<PlayerCondensed> AvailablePlayers { get; set; }
    public List<string> UserIds { get; set; }
}
