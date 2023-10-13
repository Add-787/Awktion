namespace Awktion.API.Models;

public class Room {
    public required int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public RoomStatus Status { get; set; }

}

public enum RoomStatus{
    Open,Closed
};