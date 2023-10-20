using Awktion.Domain.Models;
using Awktion.Domain.Repos;

namespace Awktion.Domain.Services;

public class RoomService
{
    private readonly Repo<Room> RoomRepo;
    private readonly Repo<Player> PlayerRepo;


    public RoomService(Repo<Room> roomRepo, Repo<Player> playerRepo)
    {
        RoomRepo = roomRepo;
        PlayerRepo = playerRepo;
    }

    public void CreateNewRoom(Room newRoom)
    {
        RoomRepo.Add(newRoom);
    }

    public IList<Player> ListPlayers()
    {
        return PlayerRepo.GetAll();
    }

    public void AddUserToRoom(int roomId, User user)
    {
        var room = RoomRepo.Get(roomId);
        room.AddUser(user);
    }



}
