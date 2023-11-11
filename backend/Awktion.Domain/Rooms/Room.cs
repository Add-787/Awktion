using Awktion.Domain.Games;
using Awktion.Domain.Models;

namespace Awktion.Domain.Rooms;

/// <summary>
/// Room class will be used as the class to create the signalR groups.
/// All the users added to a room, will receive all the messages in the room. 
/// </summary>
public class Room {
    public int Id { get; set; }
    public string Name { get; set; }
    private List<User> Users { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    private RoomStatus Status { get; set; } = RoomStatus.OpenRoom;

    public Room(string name)
    {
        Name = name;
    }

    public Room(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public List<User> GetUsers() 
    {
        return Users;
    }


    public void CloseRoom()
    {
        if(Status == RoomStatus.OpenRoom)
        {
            Status = RoomStatus.ClosedRoom;
        }
    }

}

public enum RoomStatus{
    OpenRoom,GameStarted,ClosedRoom
};