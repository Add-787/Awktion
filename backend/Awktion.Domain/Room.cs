using Awktion.Domain.Models;

namespace Awktion.Domain;

/// <summary>
/// Room class will be used as the class to create the signalR groups.
/// All the users added to a room, will receive all the messages in the room. 
/// </summary>
public class Room {
    public int Id { get; set; }
    public string Name { get; set; }
    private List<User> Users { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Open;

    public Room(string name)
    {
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
        if(Status == RoomStatus.Open)
        {
            Status = RoomStatus.Closed;
        }
    }

}

public enum RoomStatus{
    Open,Closed
};