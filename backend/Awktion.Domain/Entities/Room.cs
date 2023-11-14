using Awktion.Domain.Common;

namespace Awktion.Domain.Entities;

public class Room : BaseEntity
{
    public string Name { get; set; }
    public User Admin { get; set; }
    public RoomStatus Status { get; set; }
}

public enum RoomStatus {
    RoomOpened, GameStarted, RoomClosed
}