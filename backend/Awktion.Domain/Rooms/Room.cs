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

    public Game? Game { get; set; } = null;
    private List<User> Users { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    private RoomStatus Status { get; set; } = RoomStatus.Open;

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

    public IList<User> GetUsers() 
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

    public bool StartGame()
    {
        CloseRoom();

        if(Game == null) { return false; }

        Game.Start();
        return true;
    }

}

public enum RoomStatus{
    Open,Closed
};