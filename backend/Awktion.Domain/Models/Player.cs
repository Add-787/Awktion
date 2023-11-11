using System;
namespace Awktion.Domain.Models;

public record Player {
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required Position Pos { get; init; }
    public required int BasePrice { get; init; }
    public required int Age { get; init; }
    public required string Country { get; init; }
    public PlayerStatus Status { get; set;} = PlayerStatus.Waiting;
}

public enum Position {
    FW,MID,DEF,GK
}

public enum PlayerStatus {
    Waiting,
    Unsold,
    Sold,
}
