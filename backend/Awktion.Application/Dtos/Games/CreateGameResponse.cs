using Awktion.Domain.Entities;

namespace Awktion.Application.Dtos.Games;

public class CreateGameResponse
{
    public int RoomId { get; set; } 
    public RoomStatus Status { get; set; }
}
