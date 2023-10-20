using Awktion.Domain.Games;
using Awktion.Domain.Models;
using Awktion.Domain.Repos;

namespace Awktion.Domain.Services;

public class GameService
{
    private readonly Repo<Room> RoomRepo;
    public GameService(Repo<Room> roomRepo)
    {
        RoomRepo = roomRepo;
    }

    public void StartGame(int roomId, GameSettings settings)
    {
        var room = RoomRepo.Get(roomId);

        room.StartGame(settings);
    }

    
}
