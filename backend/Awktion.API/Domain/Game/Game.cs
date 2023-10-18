using Awktion.API.Domain.Game.Models;
using Awktion.API.Models;

namespace Awktion.API.Domain.Game;

public class Game
{
    public Room Room { get; set; }
    private GameSettings Settings { get; set;  }
    private readonly Dictionary<User, int> Balances = new();

    public Game(Room room, GameSettings settings)
    {
        Room = room;
        Settings = settings;
    }

    public void InitBalances()
    {
        foreach(User user in Room.GetUsers())
        {
            Balances.Add(user, Settings.TotalBalance);
        }

    }




}
