using Awktion.Domain.Models;
using Awktion.Domain.Repos;
using Awktion.Domain.Rooms;
using Awktion.Domain.Services;

namespace Awktion.Tests.Services;

public class FakeRoomService : IRoomService
{
    private readonly Repo<Room> RoomRepo;
    public FakeRoomService(Repo<Room> roomRepo)
    {
        RoomRepo = roomRepo;
    }

    public void AddUserToRoom(int roomId, User user)
    {
        var room = RoomRepo.Get(roomId) ?? throw new NotFoundException<Room>();

        room.AddUser(user);
    }

    public void CreateNewRoom(Room newRoom)
    {
        RoomRepo.Add(newRoom);
    }

    public Room GetRoom(int roomId)
    {
        return RoomRepo.Get(roomId);
    }

}
