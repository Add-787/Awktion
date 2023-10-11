namespace Awktion.API.Models;

public class Room {
    public string Id { get; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public RoomStatus Status { get; set; }

}

public enum RoomStatus{
    Open,Closed
};