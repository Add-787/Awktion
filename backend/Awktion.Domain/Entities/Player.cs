using Awktion.Domain.Common;

namespace Awktion.Domain.Entities;

public class Player  : BaseEntity {
    public required string Name { get; init; }
    public required Position Pos { get; init; }
    public required int BasePrice { get; init; }
    public required int Age { get; init; }
    public required string Country { get; init; }
}

public enum Position {
    FW,MID,DEF,GK
}

public enum PlayerStatus {
    Waiting,
    Unsold,
    Sold,
}

