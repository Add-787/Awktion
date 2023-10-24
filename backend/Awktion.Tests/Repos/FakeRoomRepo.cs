
using Awktion.Domain.Repos;
using Awktion.Domain.Rooms;

namespace Awktion.Tests.Repos;

/// <summary>
/// Room repository for testing purposes.
/// </summary>
public class FakeRoomRepo : Repo<Room>
{
    private readonly IList<Room> Rooms = new List<Room>
    {
        new(1,"fakeroom1"),
        new(2,"fakeroom2"),
        new(3,"fakeroom3")
    };

    public void Add(Room room)
    {
        if(Rooms.Where(r => r.Id == room.Id).Any())
        {
            throw new AlreadyExistsException<Room>();
        }

        Rooms.Add(room);
    }

    public Room Get(int Id)
    {
        var foundRoom = Rooms.FirstOrDefault(r => r.Id == Id) ?? throw new NotFoundException<Room>();

        return foundRoom;
    }

    public IList<Room> GetAll()
    {
        var rooms = Rooms ?? throw new Exception();
        return rooms;
    }

    public void Update(Room updatedRoom)
    {
        var updateRoom = Rooms.FirstOrDefault(r => r.Id == updatedRoom.Id) ?? throw new NotFoundException<Room>();

        Rooms.Remove(updateRoom);

        Rooms.Add(updatedRoom);
    }

}
