using Awktion.Domain.Models;

namespace Awktion.Domain;

public class Room {
    public int ID { get; set; }
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