using Awktion.Domain.Games;
using Awktion.Domain.Models;
using Awktion.Domain.Repos;
using Awktion.Domain.Rooms;

namespace Awktion.Domain.Services;

public class GameService
{
    private readonly Repo<Room> RoomRepo;
    public GameService(Repo<Room> roomRepo)
    {
        RoomRepo = roomRepo;
    }


    public void StartNewGame(int roomId, GameSettings settings)
    {
        var room = RoomRepo.Get(roomId);

        //TODO: Put this inside room class.
        var newGame = new Game(settings, room.GetUsers());

        room.Game = newGame;

        room.StartGame();

    }



    
}
